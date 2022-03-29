using VirtoCommerce.ExperienceApiModule.Core.Infrastructure;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Services;

namespace VirtoCommerce.Loyalty.Xapi.Queries
{
    public class GetUserBalanceQueryHandler : IQueryHandler<GetUserBalanceQuery, UserBalance>
    {
        private readonly ILoyaltyService _loyaltyService;

        public GetUserBalanceQueryHandler(ILoyaltyService loyaltyService)
        {
            _loyaltyService = loyaltyService;
        }

        public async Task<UserBalance> Handle(GetUserBalanceQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }

            UserBalance result = await _loyaltyService.GetUserBalanceAsync(request.UserId, request.StoreId, request.ShowOperations);

            return result;
        }
    }
}
