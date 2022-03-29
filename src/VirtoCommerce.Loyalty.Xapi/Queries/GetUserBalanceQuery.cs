using GraphQL;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.Loyalty.Core.Models;

namespace VirtoCommerce.Loyalty.Xapi.Queries
{
    public class GetUserBalanceQuery : IQuery<UserBalance>, IExtendableQuery
    {
        public string UserId { get; set; }
        public string StoreId { get; set; }
        public bool ShowOperations { get; set; }

        public GetUserBalanceQuery()
        {
        }

        public GetUserBalanceQuery(string userId, string storeId, bool showOperations)
        {
            UserId = userId;
            StoreId = storeId;
            ShowOperations = showOperations;
        }

        public void Map(IResolveFieldContext context)
        {
            UserId = context.GetArgument<string>("userId");
            StoreId = context.GetArgument<string>("storeId");
            ShowOperations = context.GetArgument<bool>("showOperations");
        }
    }
}
