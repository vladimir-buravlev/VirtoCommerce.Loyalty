using GraphQL.Types;
using VirtoCommerce.ExperienceApiModule.Core.Schemas;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class OperationsQueryConnectionArguments : ArgumentList
    {
        public OperationsQueryConnectionArguments()
        {
            Argument<StringGraphType>("filter", "This parameter applies a filter to the query results");
            Argument<StringGraphType>("sort", "The sort expression");
            Argument<StringGraphType>("cultureName", "Culture name (\"en-US\")");
            Argument<NonNullGraphType<StringGraphType>>("userId", "User ID");
            Argument<StringGraphType>("storeId", "Store ID (may be nullable)");
        }

        public virtual OperationsQueryConnectionArguments AddArguments(QueryArguments arguments)
        {
            foreach (var argument in arguments)
                Add(argument);
            return this;
        }
    }
}
