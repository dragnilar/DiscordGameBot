﻿using AutomoderatorGameBot.BackEnd.Configuration;
using AutomoderatorGameBot.BackEnd.Models;
using Config.Net;
using Microsoft.EntityFrameworkCore;

namespace AutomoderatorGameBot.BackEnd.DbContexts
{
    public class GameDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        public DbSet<CoolDown> CoolDowns { get; set; }
        public DbSet<MiniGameChoice> MiniGameChoices { get; set; }
        public DbSet<BotConfig> BotConfigs { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<ClassEquipment> ClassEquipments { get; set; }
        public DbSet<ItemEffect> ItemEffects {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var settings = new ConfigurationBuilder<IAutomoderatorGameBotBackEndConfig>()
                .UseJsonConfig()
                .Build();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(settings.ConnectionString);
        }
    }
}