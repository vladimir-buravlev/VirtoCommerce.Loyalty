using System;
using System.ComponentModel.DataAnnotations;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Data.Models
{
    public class UserBalanceEntity : AuditableEntity
    {
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string StoreId { get; set; }

        public decimal Balance { get; set; }


        public virtual UserBalance ToModel(UserBalance userBalance)
        {
            if (userBalance == null)
            {
                throw new ArgumentNullException(nameof(userBalance));
            }

            userBalance.Id = Id;
            userBalance.CreatedBy = CreatedBy;
            userBalance.CreatedDate = CreatedDate;
            userBalance.ModifiedBy = ModifiedBy;
            userBalance.ModifiedDate = ModifiedDate;

            userBalance.UserId = UserId;
            userBalance.StoreId = StoreId;
            userBalance.Balance = Balance;

            return userBalance;
        }

        public UserBalanceEntity FromModel(UserBalance userBalance, PrimaryKeyResolvingMap pkMap)
        {
            if (userBalance == null)
                throw new ArgumentNullException(nameof(userBalance));

            pkMap.AddPair(userBalance, this);

            Id = userBalance.Id;
            CreatedBy = userBalance.CreatedBy;
            CreatedDate = userBalance.CreatedDate;
            ModifiedBy = userBalance.ModifiedBy;
            ModifiedDate = userBalance.ModifiedDate;

            UserId = userBalance.UserId;
            StoreId = userBalance.StoreId;
            Balance = userBalance.Balance;

            return this;
        }
    }
}
