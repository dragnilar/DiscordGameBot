using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class DeprecatePoopCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "PoopBucks",
                "GameUsers");

            migrationBuilder.DropColumn(
                "ShitBucks",
                "GameUsers");

            migrationBuilder.AddColumn<long>(
                "ShitCoins",
                "GameUsers",
                "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ShitCoins",
                "GameUsers");

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
        }
    }
}