using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Core.Models.Search
{
    public class LoyaltySearchCriteria : SearchCriteriaBase
    {
        public string UserId { get; set; }
        public string StoreId { get; set; }
    }
}
