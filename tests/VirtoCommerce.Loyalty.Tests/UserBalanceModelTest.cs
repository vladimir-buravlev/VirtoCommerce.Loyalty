using System;
using Moq;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Platform.Core.Common;
using Xunit;

namespace VirtoCommerce.Loyalty.Tests
{
    public class UserBalanceModelTest
    {
        public UserBalanceModelTest()
        {
        }

        [Fact]
        public void ConvertUserBalanceEntityToModel_Null_ThrowsArgumentNullException()
        {
            UserBalanceEntity userBalanceEntity = new UserBalanceEntity();

            Action actual = () => userBalanceEntity.ToModel(null);

            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void ConvertUserBalanceEntityFromModel_Null_ThrowsArgumentNullException()
        {
            UserBalanceEntity userBalanceEntity = new UserBalanceEntity();

            Action actual = () => userBalanceEntity.FromModel(null, null);

            Assert.Throws<ArgumentNullException>(actual);
        }

        [Theory]
        [InlineData("0001-0001", "store01", 100)]
        [InlineData("0001-0002", "store02", 200)]
        public void ConvertUserBalanceEntityToModelFromModel_NotNull_ReturnsSameValue(string userId, string storeId, decimal balance)
        {
            UserBalanceEntity originalUserBalance = new UserBalanceEntity()
            {
                UserId = userId,
                StoreId = storeId,
                Balance = balance
            };

            UserBalance domainModelUserBalance = originalUserBalance.ToModel(new UserBalance());
            var pkMap = new Mock<PrimaryKeyResolvingMap>();
            UserBalanceEntity convertedUserBalance = new UserBalanceEntity().FromModel(domainModelUserBalance, pkMap.Object);

            Assert.Equal(originalUserBalance.UserId, convertedUserBalance.UserId);
            Assert.Equal(originalUserBalance.StoreId, convertedUserBalance.StoreId);
            Assert.Equal(originalUserBalance.Balance, convertedUserBalance.Balance);
        }
    }
}
