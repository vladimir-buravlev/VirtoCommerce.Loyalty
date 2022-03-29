using GraphQL.Types;
using VirtoCommerce.Loyalty.Core.Models;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class PointsOperationType : ObjectGraphType<PointsOperation>
    {
        public PointsOperationType()
        {
            Name = "PointsOperation";
            Description = "Represents points opertation";

            Field(x => x.UserId, nullable: false).Description("The unique ID of the user.");
            Field(x => x.StoreId, nullable: true).Description("The unique ID of the store (may be nullable).");
            Field(x => x.Reason, nullable: true).Description("The reason of points operation");
            Field(x => x.Amount, nullable: true).Description("Amount of points in operation");
            Field(x => x.IsDeposit, nullable: true).Description("Is the operaion deposit, or debit");
            Field("date", x => x.CreatedDate.ToString("yyyy.MM.dd HH:mm:ss"), nullable: true).Description("Is the operaion deposit, or debit");
            Field("balance", x => x.BalanceAfterOperation, nullable: true).Description("User balance after operation");
        }
    }
}
