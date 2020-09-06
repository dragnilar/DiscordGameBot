using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class MiniGameChoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                "Price",
                "Items",
                "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                "SellPrice",
                "Items",
                "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Price",
                "Items");

            migrationBuilder.DropColumn(
                "SellPrice",
                "Items");
        }
    }
}