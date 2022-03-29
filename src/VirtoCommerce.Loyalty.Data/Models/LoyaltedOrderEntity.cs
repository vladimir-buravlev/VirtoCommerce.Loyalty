using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Data.Model;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Data.Models
{
    public class LoyaltedOrderEntity : CustomerOrderEntity
    {
        public bool LoyaltyCalculated { get; set; }

        public override CustomerOrder ToModel(CustomerOrder customerOrder)
        {
            if (customerOrder is LoyaltedOrder loyaltedOrder)
            {
                loyaltedOrder.LoyaltyCalculated = LoyaltyCalculated;
            }

            base.ToModel(customerOrder);

            return customerOrder;
        }

        public override CustomerOrderEntity FromModel(CustomerOrder customerOrder, PrimaryKeyResolvingMap pkMap)
        {
            if (customerOrder is LoyaltedOrder loyaltedOrder)
            {
                LoyaltyCalculated = loyaltedOrder.LoyaltyCalculated;
            }

            return base.FromModel(customerOrder, pkMap);
        }

        public override void Patch(CustomerOrderEntity target)
        {
            if (target is LoyaltedOrderEntity loyaltedOrderEtnity)
            {
                loyaltedOrderEtnity.LoyaltyCalculated = LoyaltyCalculated;
            }

            base.Patch(target);
        }
    }
}
