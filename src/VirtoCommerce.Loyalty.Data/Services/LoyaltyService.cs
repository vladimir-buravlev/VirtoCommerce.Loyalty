using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Loyalty.Core.Events;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Loyalty.Data.Repositories;
using VirtoCommerce.Platform.Core.Caching;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Events;
using VirtoCommerce.Platform.Data.GenericCrud;

namespace VirtoCommerce.Loyalty.Data.Services
{
    public class LoyaltyService : CrudService<PointsOperation, PointsOperationEntity, OperationChangeEvent, OperationChangedEvent>, ILoyaltyService
    {
        private readonly Func<ILoyaltyRepository> _repositoryFactory;

        public LoyaltyService(Func<ILoyaltyRepository> repositoryFactory, IPlatformMemoryCache platformMemoryCache, IEventPublisher eventPublisher)
            : base(repositoryFactory, platformMemoryCache, eventPublisher)
        {
            _repositoryFactory = repositoryFactory;
        }

        public virtual async Task<UserBalance> GetUserBalanceAsync(string userId, string storeId, bool showOperations)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            using var repository = _repositoryFactory();

            UserBalanceEntity userBalanceEntity = await repository.GetUserBalance(userId, storeId);
            UserBalance userBalance = userBalanceEntity?.ToModel(AbstractTypeFactory<UserBalance>.TryCreateInstance());
            if (userBalance == null)
            {
                userBalance = AbstractTypeFactory<UserBalance>.TryCreateInstance();
                userBalance.UserId = userId;
                userBalance.StoreId = storeId;
                userBalance.Balance = 0;
            }
            if (showOperations)
            {
                userBalance.Operations = (await repository.GetUserOperations(userId, storeId))
                    .Select(x => x.ToModel(AbstractTypeFactory<PointsOperation>.TryCreateInstance())).Take(20).ToList();
            }
            return userBalance;
        }

        public virtual async Task<decimal> AddPointOperationAsync(PointsOperation pointOperation)
        {
            //we need some logic here - who can add point, how need check this?
            using var repository = _repositoryFactory();
            PrimaryKeyResolvingMap pkMap = new PrimaryKeyResolvingMap();

            UserBalanceEntity userBalanceEntity = await repository.GetUserBalance(pointOperation.UserId, pointOperation.StoreId);
            bool isNew = false;
            decimal balance = userBalanceEntity?.Balance ?? 0;

            if (userBalanceEntity == null)
            {
                isNew = true;
                userBalanceEntity = AbstractTypeFactory<UserBalanceEntity>.TryCreateInstance();
                userBalanceEntity.UserId = pointOperation.UserId;
                userBalanceEntity.StoreId = pointOperation.StoreId;
            }

            if (pointOperation.IsDeposit)
            {
                balance += pointOperation.Amount;
            }
            else
            {
                balance -= pointOperation.Amount;
            }
            userBalanceEntity.Balance = balance;
            pointOperation.BalanceAfterOperation = balance;

            await base.SaveChangesAsync(new List<PointsOperation> { pointOperation });

            if (isNew)
            {
                repository.Add(userBalanceEntity);
            }
            else
            {
                repository.Update(userBalanceEntity);
            }

            await repository.UnitOfWork.CommitAsync();

            return balance;
        }

        protected override async Task<IEnumerable<PointsOperationEntity>> LoadEntities(IRepository repository, IEnumerable<string> ids, string responseGroup)
        {
            return await ((ILoyaltyRepository)repository).PointsOperations.Where(x => ids.Contains(x.Id)).ToArrayAsync();
        }
    }
}
