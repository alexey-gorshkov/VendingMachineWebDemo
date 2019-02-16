using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachine.DAL.Migrations
{
    public partial class UserDepositOneTOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDeposits_UserId",
                table: "UserDeposits");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeposits_UserId",
                table: "UserDeposits",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserDeposits_UserId",
                table: "UserDeposits");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeposits_UserId",
                table: "UserDeposits",
                column: "UserId");
        }
    }
}
