using Microsoft.EntityFrameworkCore.Migrations;
using VirtoCommerce.Loyalty.Data.Models;

#nullable disable

namespace VirtoCommerce.Loyalty.Data.Migrations
{
    public partial class AddLoyaltyCalculated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CustomerOrder",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: nameof(LoyaltedOrderEntity));

            migrationBuilder.AddColumn<bool>(
                name: "LoyaltyCalculated",
                table: "CustomerOrder",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "LoyaltyCalculated",
                table: "CustomerOrder");
        }
    }
}
