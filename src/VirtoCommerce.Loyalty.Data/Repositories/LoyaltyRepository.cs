using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.OrdersModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.Loyalty.Data.Repositories
{
    public class LoyaltyRepository : OrderRepository, ILoyaltyRepository, IOrderRepository
    {
        public IQueryable<UserBalanceEntity> UserBalances => DbContext.Set<UserBalanceEntity>();
        public IQueryable<PointsOperationEntity> PointsOperations => DbContext.Set<PointsOperationEntity>();

        public LoyaltyRepository(LoyaltyDbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual async Task<UserBalanceEntity> GetUserBalance(string userId, string storeId)
        {
            return await UserBalances.FirstOrDefaultAsync(x => x.UserId == userId && x.StoreId == storeId);
        }

        public virtual async Task<PointsOperationEntity[]> GetUserOperations(string userId, string storeId)
        {
            return await PointsOperations.Where(x => x.UserId == userId && x.StoreId == storeId).OrderByDescending(x => x.CreatedDate).ToArrayAsync();
        }
    }
}
