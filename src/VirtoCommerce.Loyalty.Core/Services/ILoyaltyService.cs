using System.Threading.Tasks;
using VirtoCommerce.Loyalty.Core.Models;

namespace VirtoCommerce.Loyalty.Core.Services
{
    public interface ILoyaltyService
    {
        Task<UserBalance> GetUserBalanceAsync(string userId, string storeId, bool showOperations);
        Task<decimal> AddPointOperationAsync(PointsOperation pointOperation);
    }
}
