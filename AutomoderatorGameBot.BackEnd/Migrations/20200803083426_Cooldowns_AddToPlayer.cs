using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class Cooldowns_AddToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "CoolDownId",
                "GameUsers",
                "int",
                nullable: true);

            migrationBuilder.CreateTable(
                "CoolDowns",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MineLastUsed = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_CoolDowns", x => x.Id); });

            migrationBuilder.CreateIndex(
                "IX_GameUsers_CoolDownId",
                "GameUsers",
                "CoolDownId");

            migrationBuilder.AddForeignKey(
                "FK_GameUsers_CoolDowns_CoolDownId",
                "GameUsers",
                "CoolDownId",
                "CoolDowns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_GameUsers_CoolDowns_CoolDownId",
                "GameUsers");

            migrationBuilder.DropTable(
                "CoolDowns");

            migrationBuilder.DropIndex(
                "IX_GameUsers_CoolDownId",
                "GameUsers");

            migrationBuilder.DropColumn(
                "CoolDownId",
                "GameUsers");
        }
    }
}