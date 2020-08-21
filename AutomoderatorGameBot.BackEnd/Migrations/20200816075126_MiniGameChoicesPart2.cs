using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class MiniGameChoicesPart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MiniGameChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialResultText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegularResultText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailResultText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiniGameName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialResultMoney = table.Column<long>(type: "bigint", nullable: false),
                    RegularResultMoney = table.Column<long>(type: "bigint", nullable: false),
                    SpecialResultChance = table.Column<int>(type: "int", nullable: false),
                    RegularResultChance = table.Column<int>(type: "int", nullable: false),
                    FailResultChance = table.Column<int>(type: "int", nullable: false),
                    SpecialRewardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniGameChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MiniGameChoices_Items_SpecialRewardId",
                        column: x => x.SpecialRewardId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MiniGameChoices_SpecialRewardId",
                table: "MiniGameChoices",
                column: "SpecialRewardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MiniGameChoices");
        }
    }
}
