using System;
using System.ComponentModel.DataAnnotations;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Domain;

namespace VirtoCommerce.Loyalty.Data.Models
{
    public class PointsOperationEntity : AuditableEntity, IDataEntity<PointsOperationEntity, PointsOperation>
    {
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }

        [StringLength(128)]
        public string StoreId { get; set; }

        [StringLength(1024)]
        public string Reason { get; set; }

        public decimal Amount { get; set; }

        public bool IsDeposit { get; set; }

        public decimal BalanceAfterOperation { get; set; }

        public virtual PointsOperation ToModel(PointsOperation pointsOperation)
        {
            if (pointsOperation == null)
            {
                throw new ArgumentNullException(nameof(pointsOperation));
            }

            pointsOperation.Id = Id;
            pointsOperation.CreatedBy = CreatedBy;
            pointsOperation.CreatedDate = CreatedDate;
            pointsOperation.ModifiedBy = ModifiedBy;
            pointsOperation.ModifiedDate = ModifiedDate;

            pointsOperation.UserId = UserId;
            pointsOperation.StoreId = StoreId;
            pointsOperation.Reason = Reason;
            pointsOperation.Amount = Amount;
            pointsOperation.IsDeposit = IsDeposit;
            pointsOperation.BalanceAfterOperation = BalanceAfterOperation;

            return pointsOperation;
        }

        public PointsOperationEntity FromModel(PointsOperation pointsOperation, PrimaryKeyResolvingMap pkMap)
        {
            if (pointsOperation == null)
                throw new ArgumentNullException(nameof(pointsOperation));

            pkMap.AddPair(pointsOperation, this);

            Id = pointsOperation.Id;
            CreatedBy = pointsOperation.CreatedBy;
            CreatedDate = pointsOperation.CreatedDate;
            ModifiedBy = pointsOperation.ModifiedBy;
            ModifiedDate = pointsOperation.ModifiedDate;

            UserId = pointsOperation.UserId;
            StoreId = pointsOperation.StoreId;
            Reason = pointsOperation.Reason;
            Amount = pointsOperation.Amount;
            IsDeposit = pointsOperation.IsDeposit;
            BalanceAfterOperation = pointsOperation.BalanceAfterOperation;

            return this;
        }

        public void Patch(PointsOperationEntity target)
        {
            if (target == null)
                throw new ArgumentException(@"target argument must be of type PointsOperationEntity", nameof(target));

            target.UserId = UserId;
            target.StoreId = StoreId;
            target.Reason = Reason;
            target.Amount = Amount;
            target.IsDeposit = IsDeposit;
            target.BalanceAfterOperation = BalanceAfterOperation;
        }
    }
}
