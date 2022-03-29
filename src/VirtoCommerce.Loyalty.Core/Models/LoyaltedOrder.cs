using VirtoCommerce.OrdersModule.Core.Model;

namespace VirtoCommerce.Loyalty.Core.Models
{
    public class LoyaltedOrder : CustomerOrder
    {
        public LoyaltedOrder()
        {
            OperationType = nameof(CustomerOrder);
        }

        public bool LoyaltyCalculated { get; set; }
    }
}
