using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using VirtoCommerce.Loyalty.Data.BackgroundJobs;
using VirtoCommerce.OrdersModule.Core.Events;
using VirtoCommerce.Platform.Core.Events;

namespace VirtoCommerce.Loyalty.Data.Handlers
{
    public class OrderStatusChangedEventHandler : IEventHandler<OrderChangedEvent>
    {
        public Task Handle(OrderChangedEvent message)
        {
            var jobArguments = message.ChangedEntries
                .Where(x => x.NewEntry.Status != x.OldEntry.Status && x.NewEntry.Status == "Completed")
                .Select(x => x.NewEntry.Id)
                .ToArray();

            if (jobArguments.Any())
            {
                BackgroundJob.Enqueue<LoyaltyJob>(loyaltyJob => loyaltyJob.AccrueLoyaltyPoints(jobArguments));
            }

            return Task.CompletedTask;
        }
    }
}
