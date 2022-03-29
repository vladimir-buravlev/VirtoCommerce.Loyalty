using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Models.Search;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Loyalty.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.GenericCrud;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.Loyalty.Data.Services
{
    public class LoyaltySearchService : SearchService<LoyaltySearchCriteria, LoyaltySearchResult, PointsOperation, PointsOperationEntity>, ILoyaltySearchService
    {
        public LoyaltySearchService(Func<ILoyaltyRepository> repositoryFactory, IPlatformMemoryCache platformMemoryCache, ICrudService<PointsOperation> loyaltySearchService)
            : base(repositoryFactory, platformMemoryCache, loyaltySearchService)
        {
        }

        public Task<LoyaltySearchResult> SearchOperationsAsync(LoyaltySearchCriteria criteria)
        {
            return SearchAsync(criteria);
        }

        protected override IQueryable<PointsOperationEntity> BuildQuery(IRepository repository, LoyaltySearchCriteria criteria)
        {
            var query = ((ILoyaltyRepository)repository).PointsOperations;

            if (!string.IsNullOrEmpty(criteria.UserId))
            {
                query = query.Where(x => x.UserId == criteria.UserId);
            }

            if (!string.IsNullOrEmpty(criteria.StoreId))
            {
                query = query.Where(x => x.StoreId == criteria.StoreId);
            }
            else
            {
                query = query.Where(x => x.StoreId == null);
            }

            return query;
        }

        protected override IList<SortInfo> BuildSortExpression(LoyaltySearchCriteria criteria)
        {
            var sortInfos = criteria.SortInfos;
            if (sortInfos.IsNullOrEmpty())
            {
                sortInfos = new[]
                {
                    new SortInfo
                    {
                        SortColumn = nameof(PointsOperationEntity.CreatedDate),
                        SortDirection = SortDirection.Descending
                    }
                };
            }
            return sortInfos;
        }
    }
}
