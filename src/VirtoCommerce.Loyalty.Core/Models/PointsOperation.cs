using System;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Core.Models
{
    public class PointsOperation : AuditableEntity, ICloneable
    {
        public string UserId { get; set; }
        public string StoreId { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public bool IsDeposit { get; set; }
        public decimal BalanceAfterOperation { get; set; }

        public object Clone()
        {
            var result = base.MemberwiseClone() as PointsOperation;

            return result;
        }
    }
}
