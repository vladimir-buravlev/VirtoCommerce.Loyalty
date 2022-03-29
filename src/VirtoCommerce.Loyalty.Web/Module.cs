using System;
using System.Linq;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Loyalty.Core;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Models.Search;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.Loyalty.Data.Handlers;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Loyalty.Data.Repositories;
using VirtoCommerce.Loyalty.Data.Services;
using VirtoCommerce.Loyalty.Xapi.Extensions;
using VirtoCommerce.OrdersModule.Core.Events;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Data.Model;
using VirtoCommerce.OrdersModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Bus;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.Loyalty.Web
{
    public class Module : IModule, IHasConfiguration
    {
        public ManifestModuleInfo ModuleInfo { get; set; }
        public IConfiguration Configuration { get; set; }

        public void Initialize(IServiceCollection serviceCollection)
        {
            // database initialization
            serviceCollection.AddDbContext<LoyaltyDbContext>((provider, options) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                options.UseSqlServer(configuration.GetConnectionString(ModuleInfo.Id) ?? configuration.GetConnectionString("VirtoCommerce"));
            });

            serviceCollection.AddTransient<IOrderRepository, LoyaltyRepository>();
            serviceCollection.AddTransient<ILoyaltyRepository, LoyaltyRepository>();
            serviceCollection.AddTransient<Func<ILoyaltyRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetRequiredService<ILoyaltyRepository>());

            serviceCollection.AddTransient<ILoyaltyService, LoyaltyService>();
            serviceCollection.AddTransient<ILoyaltySearchService, LoyaltySearchService>();
            //serviceCollection.AddTransient<CustomerOrderService, LoyaltedOrderService>();
            //serviceCollection.AddTransient<ILoyaltedOrderService, LoyaltedOrderService>();

            serviceCollection.AddTransient<ISearchService<LoyaltySearchCriteria, LoyaltySearchResult, PointsOperation>, LoyaltySearchService>();
            serviceCollection.AddTransient(x => (ILoyaltySearchService)x.GetRequiredService<ISearchService<LoyaltySearchCriteria, LoyaltySearchResult, PointsOperation>>());
            serviceCollection.AddTransient(x => (ICrudService<PointsOperation>)x.GetService<ILoyaltyService>());

            serviceCollection.AddTransient<OrderStatusChangedEventHandler>();

            var graphQlBuilder = serviceCollection.AddGraphQL(_ =>
            {
                _.EnableMetrics = false;
            })
            .AddErrorInfoProvider(options =>
            {
                options.ExposeExtensions = true;
                options.ExposeExceptionStackTrace = true;
            })
            .AddRelayGraphTypes()
            .AddDataLoader();

            serviceCollection.AddXLoyalty(graphQlBuilder);

            AbstractTypeFactory<CustomerOrderEntity>.OverrideType<CustomerOrderEntity, LoyaltedOrderEntity>();
            AbstractTypeFactory<CustomerOrder>.OverrideType<CustomerOrder, LoyaltedOrder>();
        }

        public void PostInitialize(IApplicationBuilder appBuilder)
        {
            //AbstractTypeFactory<CustomerOrder>.OverrideType<CustomerOrder, LoyaltedOrder>().MapToType<LoyaltedOrderEntity>(); // ???

            // register settings
            var settingsRegistrar = appBuilder.ApplicationServices.GetRequiredService<ISettingsRegistrar>();
            settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

            // register permissions
            var permissionsProvider = appBuilder.ApplicationServices.GetRequiredService<IPermissionsRegistrar>();
            permissionsProvider.RegisterPermissions(ModuleConstants.Security.Permissions.AllPermissions.Select(x =>
                new Permission()
                {
                    GroupName = "Loyalty",
                    ModuleId = ModuleInfo.Id,
                    Name = x
                }).ToArray());


            var inProcessBus = appBuilder.ApplicationServices.GetService<IHandlerRegistrar>();
            inProcessBus.RegisterHandler<OrderChangedEvent>((message, token) => appBuilder.ApplicationServices.GetService<OrderStatusChangedEventHandler>().Handle(message));

            // Ensure that any pending migrations are applied
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<LoyaltyDbContext>())
                {
                    dbContext.Database.EnsureCreated();
                    dbContext.Database.Migrate();
                }
            }

            // add http for Schema at default url /graphql
            appBuilder.UseGraphQL<ISchema>(); //??

            // use graphql-playground at default url /ui/playground
            appBuilder.UseGraphQLPlayground(); //??
        }

        public void Uninstall()
        {
            // do nothing in here
        }
    }
}
