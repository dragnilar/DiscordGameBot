using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class MiniGameChoicesPart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "MiniGameChoices",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoiceName = table.Column<string>("nvarchar(max)", nullable: true),
                    SpecialResultText = table.Column<string>("nvarchar(max)", nullable: true),
                    RegularResultText = table.Column<string>("nvarchar(max)", nullable: true),
                    FailResultText = table.Column<string>("nvarchar(max)", nullable: true),
                    MiniGameName = table.Column<string>("nvarchar(max)", nullable: true),
                    SpecialResultMoney = table.Column<long>("bigint", nullable: false),
                    RegularResultMoney = table.Column<long>("bigint", nullable: false),
                    SpecialResultChance = table.Column<int>("int", nullable: false),
                    RegularResultChance = table.Column<int>("int", nullable: false),
                    FailResultChance = table.Column<int>("int", nullable: false),
                    SpecialRewardId = table.Column<int>("int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniGameChoices", x => x.Id);
                    table.ForeignKey(
                        "FK_MiniGameChoices_Items_SpecialRewardId",
                        x => x.SpecialRewardId,
                        "Items",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_MiniGameChoices_SpecialRewardId",
                "MiniGameChoices",
                "SpecialRewardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "MiniGameChoices");
        }
    }
}