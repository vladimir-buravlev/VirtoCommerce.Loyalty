using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Core.Models
{
    public class UserBalance : AuditableEntity
    {
        public string UserId { get; set; }
        public string StoreId { get; set; }
        public decimal Balance { get; set; }
        public IList<PointsOperation> Operations { get; set; }

        public UserBalance()
        {
            Operations = new List<PointsOperation>();
        }
    }
}
