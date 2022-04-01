using System.Linq;
using System.Threading.Tasks;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Data.Repositories
{
    public interface ILoyaltyRepository : IRepository
    {
        IQueryable<PointsOperationEntity> PointsOperations { get; }
        Task<UserBalanceEntity> GetUserBalance(string userId, string storeId);
        Task<PointsOperationEntity[]> GetUserOperations(string userId, string storeId);
    }
}
