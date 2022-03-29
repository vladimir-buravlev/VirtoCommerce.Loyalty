using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VirtoCommerce.Loyalty.Data.Repositories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LoyaltyDbContext>
    {
        public LoyaltyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LoyaltyDbContext>();

            builder.UseSqlServer("Data Source=DESKTOP-2EEHPNV\\SQLEXPRESS;Initial Catalog=VCEducation;Persist Security Info=True;User ID=virto;Password=virto;MultipleActiveResultSets=True;Connect Timeout=30");

            return new LoyaltyDbContext(builder.Options);
        }
    }
}
