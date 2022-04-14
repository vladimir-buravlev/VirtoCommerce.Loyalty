using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.Loyalty.Data.Migrations
{
    public partial class AddPointsOperationForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserBalanceId",
                table: "LoyaltyOperations",
                type: "nvarchar(128)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyOperations_UserBalanceId",
                table: "LoyaltyOperations",
                column: "UserBalanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyaltyOperations_LoyaltyBalance_UserBalanceId",
                table: "LoyaltyOperations",
                column: "UserBalanceId",
                principalTable: "LoyaltyBalance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyaltyOperations_LoyaltyBalance_UserBalanceId",
                table: "LoyaltyOperations");

            migrationBuilder.DropIndex(
                name: "IX_LoyaltyOperations_UserBalanceId",
                table: "LoyaltyOperations");

            migrationBuilder.DropColumn(
                name: "UserBalanceId",
                table: "LoyaltyOperations");
        }
    }
}
