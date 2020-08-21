using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class Cooldowns_AddToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoolDownId",
                table: "GameUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoolDowns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MineLastUsed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoolDowns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameUsers_CoolDownId",
                table: "GameUsers",
                column: "CoolDownId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameUsers_CoolDowns_CoolDownId",
                table: "GameUsers",
                column: "CoolDownId",
                principalTable: "CoolDowns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameUsers_CoolDowns_CoolDownId",
                table: "GameUsers");

            migrationBuilder.DropTable(
                name: "CoolDowns");

            migrationBuilder.DropIndex(
                name: "IX_GameUsers_CoolDownId",
                table: "GameUsers");

            migrationBuilder.DropColumn(
                name: "CoolDownId",
                table: "GameUsers");
        }
    }
}
