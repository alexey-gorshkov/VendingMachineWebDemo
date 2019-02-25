using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachine.DAL.Migrations
{
    public partial class AddPriceCreatorVm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "VMCreators",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "VMCreators");
        }
    }
}
