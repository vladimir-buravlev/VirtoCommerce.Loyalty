using System.Threading.Tasks;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Core.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Data.Jobs
{
    public class LoyaltyJob
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly ICustomerOrderService _orderService;

        public LoyaltyJob(ILoyaltyService loyaltyService, ICustomerOrderService customerOrderService)
        {
            _loyaltyService = loyaltyService;
            _orderService = customerOrderService;
        }

        public async Task AccrueLoyaltyPoints(string[] orderIds)
        {
            var orders = await _orderService.GetByIdsAsync(orderIds);
            foreach (CustomerOrder order in orders)
            {
                PointsOperation orderBonus = AbstractTypeFactory<PointsOperation>.TryCreateInstance();
                orderBonus.UserId = order.CustomerId;
                orderBonus.StoreId = order.StoreId;
                orderBonus.Amount = order.Sum;
                orderBonus.Reason = $"Completed order {order.Number} bonus";
                orderBonus.IsDeposit = true;

                await _loyaltyService.AddPointOperationAsync(orderBonus);

                LoyaltedOrder loyaltedOrder = order as LoyaltedOrder;
                loyaltedOrder.LoyaltyCalculated = true;
            }
            await _orderService.SaveChangesAsync(orders);
        }
    }
}
