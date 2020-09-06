using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "GameUsers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscordUserId = table.Column<decimal>("decimal(20,0)", nullable: false),
                    DateTimeAdded = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_GameUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "DraftedUsers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameUserId = table.Column<int>("int", nullable: false),
                    DraftDateTime = table.Column<DateTime>("datetime2", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "DraftedUsers");

            migrationBuilder.DropTable(
                "GameUsers");
        }
    }
}