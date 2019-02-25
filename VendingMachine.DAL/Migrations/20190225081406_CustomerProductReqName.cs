using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachine.DAL.Migrations
{
    public partial class CustomerProductReqName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerProducts",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
