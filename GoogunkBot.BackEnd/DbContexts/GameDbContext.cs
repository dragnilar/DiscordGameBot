using System;
using System.Collections.Generic;
using System.Text;
using Config.Net;
using GoogunkBot.BackEnd.Configuration;
using GoogunkBot.BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace GoogunkBot.BackEnd.DbContexts
{
    public class GameDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var settings = new ConfigurationBuilder<IGoogunkBackendConfig>()
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
