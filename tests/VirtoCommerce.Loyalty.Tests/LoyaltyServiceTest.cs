using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Core.Services;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Loyalty.Data.Repositories;
using VirtoCommerce.Loyalty.Data.Services;
using Xunit;

namespace VirtoCommerce.Loyalty.Tests
{
    public class LoyaltyServiceTest
    {
        private ILoyaltyRepository _loyaltyRepository;

        public LoyaltyServiceTest()
        {
        }

        [Theory]
        [InlineData("0001-0001", "store01", 100)]
        [InlineData("0001-0001", null, 1000)]
        [InlineData("0001-0002", "store02", 200)]
        [InlineData("0001-0002", null, 2000)]
        [InlineData("unknown_user", "unknown_store", 0)]
        public async Task GetUserBalance_SomeIds_ReturnsItem(string userId, string storeId, decimal expectedBalance)
        {
            MockRepository(userId, storeId);

            UserBalance userBalance = await LoyaltyService.GetUserBalanceAsync(userId, storeId, true);
            decimal factBalance = userBalance.Balance;
            decimal calculatedBalance = userBalance.Operations.Sum(x => x.Amount * (x.IsDeposit ? 1 : -1));

            Assert.Equal(expectedBalance, factBalance);
            Assert.Equal(factBalance, calculatedBalance);
        }

        [Theory]
        [InlineData("0001-0001", "store01", 100)]
        [InlineData("0001-0001", null, 1000)]
        [InlineData("0001-0002", "store02", 200)]
        [InlineData("0001-0002", null, 2000)]
        [InlineData("unknown_user", "unknown_store", 0)]
        public async Task GetUserOperations_SomeIds_ReturnsItem(string userId, string storeId, decimal expectedBalance)
        {
            MockRepository(userId, storeId);

            List<PointsOperation> pointOperations = (await LoyaltyService.GetUserOperationsAsync(userId, storeId)).ToList();
            decimal factBalance = pointOperations.Sum(x => x.Amount * (x.IsDeposit ? 1 : -1));

            Assert.Equal(expectedBalance, factBalance);
        }

        private void MockRepository(string userId, string storeId)
        {
            var loyaltyRepository = new Mock<ILoyaltyRepository>();

            UserBalanceEntity[] userBalanceEntities = TestHelper.LoadFromJsonFile<UserBalanceEntity[]>(@"UserBalances.json");
            loyaltyRepository
                .Setup(x => x.GetUserBalance(userId, storeId))
                .Returns(Task.FromResult(userBalanceEntities.FirstOrDefault(x => x.UserId == userId && x.StoreId == storeId)));

            PointsOperationEntity[] pointsOperationEntities = TestHelper.LoadFromJsonFile<PointsOperationEntity[]>(@"PointOperations.json");
            loyaltyRepository
                .Setup(x => x.GetUserOperations(userId, storeId))
                .Returns(Task.FromResult(pointsOperationEntities.Where(x => x.UserId == userId && x.StoreId == storeId).OrderByDescending(x => x.CreatedDate).ToArray()));

            _loyaltyRepository = loyaltyRepository.Object;
        }

        private ILoyaltyService LoyaltyService
        {
            get
            {
                return new LoyaltyService(() => _loyaltyRepository, null, null);
            }
        }
    }
}
