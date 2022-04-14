using Microsoft.EntityFrameworkCore;
using VirtoCommerce.Loyalty.Data.Models;
using VirtoCommerce.OrdersModule.Data.Repositories;

namespace VirtoCommerce.Loyalty.Data.Repositories
{
    public class LoyaltyDbContext : OrderDbContext
    {
        public LoyaltyDbContext(DbContextOptions<LoyaltyDbContext> options)
          : base(options)
        {
        }

        protected LoyaltyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserBalanceEntity>().ToTable("LoyaltyBalance").HasKey(x => x.Id);
            modelBuilder.Entity<UserBalanceEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();

            modelBuilder.Entity<PointsOperationEntity>().ToTable("LoyaltyOperations").HasKey(x => x.Id);
            modelBuilder.Entity<PointsOperationEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
            modelBuilder.Entity<PointsOperationEntity>().HasOne(x => x.UserBalance).WithMany(x => x.Operations)
                .HasForeignKey(x => x.UserBalanceId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LoyaltedOrderEntity>().HasDiscriminator<string>("Discriminator");
            modelBuilder.Entity<LoyaltedOrderEntity>().Property("Discriminator").HasMaxLength(128);
            modelBuilder.Entity<LoyaltedOrderEntity>().Property("LoyaltyCalculated").HasDefaultValue(false);
        }
    }
}

