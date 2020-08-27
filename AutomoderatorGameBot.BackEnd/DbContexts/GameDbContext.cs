﻿using System;
using System.Collections.Generic;
using System.Text;
using AutomoderatorGameBot.BackEnd.Configuration;
using AutomoderatorGameBot.BackEnd.Models;
using Config.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace AutomoderatorGameBot.BackEnd.DbContexts
{
    public class GameDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var settings = new ConfigurationBuilder<IAutomoderatorGameBotBackEndConfig>()
                .UseJsonConfig()
                .Build();
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(settings.ConnectionString);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<GameUser> GameUsers { get; set; }
        public DbSet<CoolDown> CoolDowns { get; set; }
        public DbSet<MiniGameChoice> MiniGameChoices { get; set; }

    }
}