using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class AddStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "DraftedUsers");

            migrationBuilder.AddColumn<bool>(
                "IsDrafted",
                "GameUsers",
                "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                "PoopBucks",
                "GameUsers",
                "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "ShitBucks",
                "GameUsers",
                "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                "Shops",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Shops", x => x.Id); });

            migrationBuilder.CreateTable(
                "Items",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    GameUserId = table.Column<int>("int", nullable: true),
                    ShopId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        "FK_Items_GameUsers_GameUserId",
                        x => x.GameUserId,
                        "GameUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Items_Shops_ShopId",
                        x => x.ShopId,
                        "Shops",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Items_GameUserId",
                "Items",
                "GameUserId");

            migrationBuilder.CreateIndex(
                "IX_Items_ShopId",
                "Items",
                "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Items");

            migrationBuilder.DropTable(
                "Shops");

            migrationBuilder.DropColumn(
                "IsDrafted",
                "GameUsers");

            migrationBuilder.DropColumn(
                "PoopBucks",
                "GameUsers");

            migrationBuilder.DropColumn(
                "ShitBucks",
                "GameUsers");

            migrationBuilder.CreateTable(
                "DraftedUsers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DraftDateTime = table.Column<DateTime>("datetime2", nullable: false),
                    GameUserId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftedUsers", x => x.Id);
                    table.ForeignKey(
                        "FK_DraftedUsers_GameUsers_GameUserId",
                        x => x.GameUserId,
                        "GameUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_DraftedUsers_GameUserId",
                "DraftedUsers",
                "GameUserId");
        }
    }
}