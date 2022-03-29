using GraphQL;
using GraphQL.Builders;
using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.Loyalty.Core.Models.Search;

namespace VirtoCommerce.Loyalty.Xapi.Queries
{
    public class SearchOperationsQuery : IQuery<LoyaltySearchResult>, IExtendableQuery
    {
        public string UserId { get; set; }
        public string StoreId { get; set; }
        public string Sort { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Filter { get; set; }

        public void Map(IResolveFieldContext context)
        {
            var connectionContext = (IResolveConnectionContext)context;
            Skip = Convert.ToInt32(connectionContext.After ?? 0.ToString());
            Take = connectionContext.First ?? connectionContext.PageSize ?? 10;
            Filter = connectionContext.GetArgument<string>("filter");
            Sort = connectionContext.GetArgument<string>("sort");
            UserId = connectionContext.GetArgument<string>("userId");
            StoreId = connectionContext.GetArgument<string>("storeId");
        }
    }
}
