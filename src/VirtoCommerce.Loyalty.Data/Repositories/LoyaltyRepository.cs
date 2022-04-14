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

        public virtual async Task<UserBalanceEntity> GetUserBalance(string userId, string storeId, bool showOperations)
        {
            var result = await UserBalances.FirstOrDefaultAsync(x => x.UserId == userId && x.StoreId == storeId);

            if (result != null && showOperations)
            {
                await PointsOperations.Where(x => x.UserId == userId && x.StoreId == storeId).OrderByDescending(x => x.CreatedDate).Skip(0).Take(20).LoadAsync();
            }

            return result;
        }
    }
}
