using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class DeprecatePoopCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoopBucks",
                table: "GameUsers");

            migrationBuilder.DropColumn(
                name: "ShitBucks",
                table: "GameUsers");

            migrationBuilder.AddColumn<long>(
                name: "ShitCoins",
                table: "GameUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShitCoins",
                table: "GameUsers");

            migrationBuilder.AddColumn<long>(
                name: "PoopBucks",
                table: "GameUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ShitBucks",
                table: "GameUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
