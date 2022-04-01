using MediatR;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Xapi.Commands
{
    public class AddPointsCommandHandler : IRequestHandler<AddPointsCommand, decimal>
    {
        private readonly ILoyaltyService _loyaltyService;

        public AddPointsCommandHandler(ILoyaltyService loyaltyService)
        {
            _loyaltyService = loyaltyService;
        }

        public async Task<decimal> Handle(AddPointsCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }

            if (string.IsNullOrEmpty(request.Reason))
            {
                throw new ArgumentNullException(nameof(request.Reason));
            }

            PointsOperation pointsOperation = AbstractTypeFactory<PointsOperation>.TryCreateInstance();
            pointsOperation.UserId = request.UserId;
            pointsOperation.StoreId = request.StoreId;
            pointsOperation.Reason = request.Reason;
            pointsOperation.Amount = Math.Abs(request.Amount);
            pointsOperation.IsDeposit = request.Amount >= 0;

            decimal result = await _loyaltyService.AddPointOperationAsync(pointsOperation);

            return result;
        }
    }
}
