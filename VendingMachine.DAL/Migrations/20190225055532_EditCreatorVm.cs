using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachine.DAL.Migrations
{
    public partial class EditCreatorVm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VMCreators_AspNetUsers_UserId",
                table: "VMCreators");

            migrationBuilder.DropForeignKey(
                name: "FK_VMCreators_VMEntities_VMEntityId",
                table: "VMCreators");

            migrationBuilder.DropIndex(
                name: "IX_VMCreators_UserId",
                table: "VMCreators");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VMCreators");

            migrationBuilder.AlterColumn<Guid>(
                name: "VMEntityId",
                table: "VMCreators",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VMCreators_VMEntities_VMEntityId",
                table: "VMCreators",
                column: "VMEntityId",
                principalTable: "VMEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VMCreators_VMEntities_VMEntityId",
                table: "VMCreators");

            migrationBuilder.AlterColumn<Guid>(
                name: "VMEntityId",
                table: "VMCreators",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "VMCreators",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VMCreators_UserId",
                table: "VMCreators",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VMCreators_AspNetUsers_UserId",
                table: "VMCreators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VMCreators_VMEntities_VMEntityId",
                table: "VMCreators",
                column: "VMEntityId",
                principalTable: "VMEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
