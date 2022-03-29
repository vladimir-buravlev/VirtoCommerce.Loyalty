using VirtoCommerce.ExperienceApiModule.XOrder;
using VirtoCommerce.ExperienceApiModule.XOrder.Queries;
using VirtoCommerce.OrdersModule.Core.Services;

namespace VirtoCommerce.Loyalty.Xapi.Queries
{
    public class GetOrderQueryExtendedHandler : GetOrderQueryHandler
    {
        public GetOrderQueryExtendedHandler(ICustomerOrderAggregateRepository customerOrderAggregateRepository, ICustomerOrderSearchService customerOrderSearchService)
            : base(customerOrderAggregateRepository, customerOrderSearchService)
        {
        }

        public override async Task<CustomerOrderAggregate> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var result = await base.Handle(request, cancellationToken);
            //result.
            return result;
        }
    }
}
