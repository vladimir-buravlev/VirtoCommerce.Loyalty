using System;
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
        //public IQueryable<LoyaltedOrderEntity> LoyaltedOrders => DbContext.Set<LoyaltedOrderEntity>();

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

        public virtual async Task<bool> SaveUserBalance(UserBalanceEntity userBalance, bool isNew)
        {
            if (userBalance != null)
            {
                if (isNew)
                {
                    DbContext.Add(userBalance);
                }
                else
                {
                    DbContext.Update(userBalance);
                }

                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public virtual async Task<bool> SavePointOperation(PointsOperationEntity pointOperation)
        {
            if (pointOperation != null)
            {
                DbContext.Add(pointOperation);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public virtual async Task<PointsOperationEntity[]> GetPointsOperationsByIds(string[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return Array.Empty<PointsOperationEntity>();
            }

            var result = await PointsOperations.Where(x => ids.Contains(x.Id)).ToArrayAsync();

            if (!result.Any())
            {
                return Array.Empty<PointsOperationEntity>();
            }

            return result;
        }
    }
}
