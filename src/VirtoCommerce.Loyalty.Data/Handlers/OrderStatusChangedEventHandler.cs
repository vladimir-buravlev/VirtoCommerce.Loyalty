using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.OrdersModule.Core.Events;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.Loyalty.Data.Handlers
{
    public class OrderStatusChangedEventHandler : IEventHandler<OrderChangedEvent>
    {
        private readonly ILoyaltyService _loyaltyService;
        private readonly ICustomerOrderService _orderService;

        public OrderStatusChangedEventHandler(ILoyaltyService loyaltyService, ICustomerOrderService customerOrderService)
        {
            _loyaltyService = loyaltyService;
            _orderService = customerOrderService;
        }

        public Task Handle(OrderChangedEvent message)
        {
            List<OrderToAccureJobArgument> jobArguments = new List<OrderToAccureJobArgument>();
            foreach (var changedEntry in message.ChangedEntries)
            {
                var oldOrder = changedEntry.OldEntry;
                var newOrder = changedEntry.NewEntry;
                bool isOrderComplete = oldOrder.Status != newOrder.Status && newOrder.Status == "Completed";
                if (isOrderComplete)
                {
                    jobArguments.Add(new OrderToAccureJobArgument { OrderId = newOrder.Id });
                }
            }

            if (jobArguments.Any())
            {
                BackgroundJob.Enqueue(() => TryToAccrueLoyaltyPoints(jobArguments.ToArray()));
            }

            return Task.CompletedTask;
        }

        public virtual async Task TryToAccrueLoyaltyPoints(OrderToAccureJobArgument[] jobArguments)
        {
            var orderIds = jobArguments.Select(x => x.OrderId).ToArray();

            var orders = await _orderService.GetByIdsAsync(orderIds);
            foreach (CustomerOrder order in orders)
            {
                PointsOperation orderBonus = AbstractTypeFactory<PointsOperation>.TryCreateInstance();
                orderBonus.UserId = order.CustomerId;
                orderBonus.StoreId = order.StoreId;
                orderBonus.Amount = order.Sum;
                orderBonus.Reason = $"Completed order {order.Number} bonus";
                orderBonus.IsDeposit = true;

                _loyaltyService.AddPointOperationAsync(orderBonus);

                LoyaltedOrder loyaltedOrder = order as LoyaltedOrder;
                loyaltedOrder.LoyaltyCalculated = true;
            }
            await _orderService.SaveChangesAsync(orders);
        }
    }
}
