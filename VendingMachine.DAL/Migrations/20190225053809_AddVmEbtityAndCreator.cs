using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachine.DAL.Migrations
{
    public partial class AddVmEbtityAndCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VMEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UserAdminId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VMEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VMEntities_AspNetUsers_UserAdminId",
                        column: x => x.UserAdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VMCreators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Availability = table.Column<int>(nullable: false),
                    TypeProduct = table.Column<int>(nullable: false),
                    CreatorClassName = table.Column<string>(nullable: true),
                    VMEntityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VMCreators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VMCreators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VMCreators_VMEntities_VMEntityId",
                        column: x => x.VMEntityId,
                        principalTable: "VMEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VMCreators_UserId",
                table: "VMCreators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VMCreators_VMEntityId",
                table: "VMCreators",
                column: "VMEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VMEntities_UserAdminId",
                table: "VMEntities",
                column: "UserAdminId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VMCreators");

            migrationBuilder.DropTable(
                name: "VMEntities");
        }
    }
}
