using GraphQL;
using GraphQL.Builders;
using GraphQL.Resolvers;
using GraphQL.Types;
using MediatR;
using VirtoCommerce.ExperienceApiModule.Core.Extensions;
using VirtoCommerce.ExperienceApiModule.Core.Helpers;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Xapi.Commands;
using VirtoCommerce.Loyalty.Xapi.Extensions;
using VirtoCommerce.Loyalty.Xapi.Queries;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class LoyaltySchema : ISchemaBuilder
    {
        private const string _commandName = "command";
        private readonly IMediator _mediator;

        public LoyaltySchema(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Build(ISchema schema)
        {
            FieldType balanceField = new FieldType();
            balanceField.Name = "balance";
            balanceField.Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId", Description = "User Id" },
                    new QueryArgument<StringGraphType> { Name = "storeId", Description = "Store Id" },
                    new QueryArgument<BooleanGraphType> { Name = "showOperations", Description = "Need show points operations in result" }
                );
            balanceField.Type = GraphTypeExtenstionHelper.GetActualType<UserBalanceType>();
            balanceField.Resolver = new AsyncFieldResolver<object>(async context =>
            {
                var request = context.ExtractQuery<GetUserBalanceQuery>();
                context.CopyArgumentsToUserContext();

                var loyaltyResponce = await _mediator.Send(request);
                context.SetExpandedObjectGraph(loyaltyResponce);

                return loyaltyResponce;
            });

            schema.Query.AddField(balanceField);

            var operationsConnectionBuilder = GraphTypeExtenstionHelper
                .CreateConnection<PointsOperationType, object>()
                .Name("pointsOperations")
                .PageSize(20)
                .Arguments();

            operationsConnectionBuilder.ResolveAsync(async context =>
            {
                var query = context.ExtractQuery<SearchOperationsQuery>();

                var response = await _mediator.Send(query);

                foreach (var operation in response.Results)
                {
                    context.SetExpandedObjectGraph(operation);
                }

                return new PagedConnection<PointsOperation>(response.Results, query.Skip, query.Take, response.TotalCount);
            });

            schema.Query.AddField(operationsConnectionBuilder.FieldType);

            var addPoints = FieldBuilder.Create<object, decimal>(typeof(DecimalGraphType))
                            .Name("addPoints")
                            .Argument(GraphTypeExtenstionHelper.GetActualComplexType<NonNullGraphType<InputPointsOperationType>>(), _commandName)
                            .ResolveAsync(async context =>
                            {
                                var type = GenericTypeHelper.GetActualType<AddPointsCommand>();
                                var command = (AddPointsCommand)context.GetArgument(type, _commandName);
                                return await _mediator.Send(command);
                            });
            schema.Mutation?.AddField(addPoints.FieldType);
        }
    }
}
