using GraphQL.Types;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class InputPointsOperationType : InputObjectGraphType
    {
        public InputPointsOperationType()
        {
            Field<NonNullGraphType<StringGraphType>>("userId", "User ID");
            Field<StringGraphType>("storeId", "Store ID (may be nullable)");
            Field<NonNullGraphType<StringGraphType>>("reason", "The reason of operation");
            Field<NonNullGraphType<DecimalGraphType>>("amount", "Amount of points");
            //Field<NonNullGraphType<BooleanGraphType>>("isDeposit", "Is operation deposit, or debit"); //now isDeposit - computational
        }
    }
}
