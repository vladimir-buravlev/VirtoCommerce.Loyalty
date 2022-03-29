using System.Threading.Tasks;
using VirtoCommerce.Loyalty.Core.Models.Search;

namespace VirtoCommerce.Loyalty.Core.Services
{
    public interface ILoyaltySearchService
    {
        Task<LoyaltySearchResult> SearchOperationsAsync(LoyaltySearchCriteria criteria);
    }
}
