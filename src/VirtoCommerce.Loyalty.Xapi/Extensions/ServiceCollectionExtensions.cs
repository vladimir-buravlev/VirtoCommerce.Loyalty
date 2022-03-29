using AutoMapper;
using GraphQL;
using GraphQL.Server;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.ExperienceApiModule.Core.Extensions;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.ExperienceApiModule.XOrder.Schemas;
using VirtoCommerce.Loyalty.Xapi.Schemas;

namespace VirtoCommerce.Loyalty.Xapi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXLoyalty(this IServiceCollection services, IGraphQLBuilder graphQlbuilder)
        {
            services.AddSingleton<ISchemaBuilder, LoyaltySchema>();

            graphQlbuilder.AddGraphTypes(typeof(XLoyaltyAnchor));

            //services.OverrideQueryType<GetOrderQuery, GetOrderQueryExtended>().WithQueryHandler<GetOrderQueryExtendedHandler>();
            services.AddSchemaType<LoyaltedOrderType>().OverrideType<CustomerOrderType, LoyaltedOrderType>();

            services.AddMediatR(typeof(XLoyaltyAnchor));

            services.AddAutoMapper(typeof(XLoyaltyAnchor));

            return services;
        }
    }
}
