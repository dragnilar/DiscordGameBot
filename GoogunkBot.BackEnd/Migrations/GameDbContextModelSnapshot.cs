﻿// <auto-generated />
using System;
using GoogunkBot.BackEnd.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoogunkBot.BackEnd.Migrations
{
    [DbContext(typeof(GameDbContext))]
    partial class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.5.20278.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.CoolDown", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("MineLastUsed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CoolDowns");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.GameUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoolDownId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DiscordUserId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<bool>("IsDrafted")
                        .HasColumnType("bit");

                    b.Property<long>("PoopBucks")
                        .HasColumnType("bigint");

                    b.Property<long>("ShitBucks")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CoolDownId");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<long>("SellPrice")
                        .HasColumnType("bigint");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameUserId");

                    b.HasIndex("ShopId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.MiniGameChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChoiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FailResultChance")
                        .HasColumnType("int");

                    b.Property<string>("FailResultText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniGameName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegularResultChance")
                        .HasColumnType("int");

                    b.Property<long>("RegularResultMoney")
                        .HasColumnType("bigint");

                    b.Property<string>("RegularResultText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecialResultChance")
                        .HasColumnType("int");

                    b.Property<long>("SpecialResultMoney")
                        .HasColumnType("bigint");

                    b.Property<string>("SpecialResultText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecialRewardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialRewardId");

                    b.ToTable("MiniGameChoices");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.GameUser", b =>
                {
                    b.HasOne("GoogunkBot.BackEnd.Models.CoolDown", "CoolDown")
                        .WithMany()
                        .HasForeignKey("CoolDownId");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.Item", b =>
                {
                    b.HasOne("GoogunkBot.BackEnd.Models.GameUser", "GameUser")
                        .WithMany("Items")
                        .HasForeignKey("GameUserId");

                    b.HasOne("GoogunkBot.BackEnd.Models.Shop", "Shop")
                        .WithMany("Items")
                        .HasForeignKey("ShopId");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.MiniGameChoice", b =>
                {
                    b.HasOne("GoogunkBot.BackEnd.Models.Item", "SpecialReward")
                        .WithMany()
                        .HasForeignKey("SpecialRewardId");
                });
#pragma warning restore 612, 618
        }
    }
}
