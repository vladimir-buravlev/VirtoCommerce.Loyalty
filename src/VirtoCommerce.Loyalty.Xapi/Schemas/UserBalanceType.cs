using GraphQL.Types;
using VirtoCommerce.Loyalty.Core.Models;

namespace VirtoCommerce.Loyalty.Xapi.Schemas
{
    public class UserBalanceType : ObjectGraphType<UserBalance>
    {
        public UserBalanceType()
        {
            Name = "UserBalance";
            Description = "Represents user balance";

            Field(x => x.UserId, nullable: false).Description("The unique ID of the user.");
            Field(x => x.StoreId, nullable: true).Description("The ID of the store (may be nullable).");
            Field(x => x.Balance, nullable: true).Description("User loyalty balance");
            Field<ListGraphType<PointsOperationType>>("operations", "Points operations", resolve: context => context.Source.Operations);
        }
    }
}
