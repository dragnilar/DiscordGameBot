﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomoderatorGameBot.BackEnd.Migrations
{
    public partial class AddBotConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "BotConfigs",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShutUpLastUsed = table.Column<DateTime>("datetime2", nullable: false),
                    ShutUpDuration = table.Column<int>("int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_BotConfigs", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BotConfigs");
        }
    }
}