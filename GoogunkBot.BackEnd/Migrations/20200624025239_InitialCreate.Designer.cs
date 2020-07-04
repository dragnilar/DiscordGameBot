﻿// <auto-generated />
using System;
using GoogunkBot.BackEnd.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoogunkBot.BackEnd.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20200624025239_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.5.20278.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.DraftedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DraftDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameUserId");

                    b.ToTable("DraftedUsers");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.GameUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeAdded")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DiscordUserId")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.ToTable("GameUsers");
                });

            modelBuilder.Entity("GoogunkBot.BackEnd.Models.DraftedUser", b =>
                {
                    b.HasOne("GoogunkBot.BackEnd.Models.GameUser", "GameUser")
                        .WithMany()
                        .HasForeignKey("GameUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}