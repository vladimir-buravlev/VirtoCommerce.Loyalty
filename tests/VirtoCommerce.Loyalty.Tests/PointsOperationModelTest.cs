using System;
using Moq;
using VirtoCommerce.Loyalty.Core.Models;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.Platform.Core.Common;
using Xunit;

namespace VirtoCommerce.Loyalty.Tests
{
    public class PointsOperationModelTest
    {
        public PointsOperationModelTest()
        {
        }

        [Fact]
        public void ConvertPointsOperationEntityToModel_Null_ThrowsArgumentNullException()
        {
            PointsOperationEntity pointsOperationEntity = new PointsOperationEntity();

            Action actual = () => pointsOperationEntity.ToModel(null);

            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void ConvertPointsOperationEntityFromModel_Null_ThrowsArgumentNullException()
        {
            PointsOperationEntity pointsOperationEntity = new PointsOperationEntity();

            Action actual = () => pointsOperationEntity.FromModel(null, null);

            Assert.Throws<ArgumentNullException>(actual);
        }

        [Theory]
        [InlineData("0001-0001", "store01", "Just for fun", 12.05, true)]
        [InlineData("0001-0002", "store02", "Another reason", 2.5, false)]
        public void ConvertPointsOperationEntityToModelFromModel_NotNull_ReturnsSameValue(string userId, string storeId, string reason, decimal amount, bool isDeposit)
        {
            PointsOperationEntity originalPointsOperation = new PointsOperationEntity()
            {
                UserId = userId,
                StoreId = storeId,
                Reason = reason,
                Amount = amount,
                IsDeposit = isDeposit
            };

            PointsOperation domainModelPointsOperation = originalPointsOperation.ToModel(new PointsOperation());
            var pkMap = new Mock<PrimaryKeyResolvingMap>();
            PointsOperationEntity convertedPointsOperation = new PointsOperationEntity().FromModel(domainModelPointsOperation, pkMap.Object);

            Assert.Equal(originalPointsOperation.UserId, convertedPointsOperation.UserId);
            Assert.Equal(originalPointsOperation.StoreId, convertedPointsOperation.StoreId);
            Assert.Equal(originalPointsOperation.Reason, convertedPointsOperation.Reason);
            Assert.Equal(originalPointsOperation.Amount, convertedPointsOperation.Amount);
            Assert.Equal(originalPointsOperation.IsDeposit, convertedPointsOperation.IsDeposit);
        }
    }
}
