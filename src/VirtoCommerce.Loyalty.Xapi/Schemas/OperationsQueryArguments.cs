using GraphQL.Types;
using VirtoCommerce.ExperienceApiModule.Core.Schemas;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class OperationsQueryArguments : ArgumentList
    {
        public OperationsQueryArguments()
        {
            Argument<NonNullGraphType<StringGraphType>>("userId", "User ID");
            Argument<StringGraphType>("storeId", "Store ID (may be nullable)");
        }
    }
}
