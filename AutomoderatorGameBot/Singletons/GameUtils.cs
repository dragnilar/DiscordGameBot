using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.DbContexts;
using AutomoderatorGameBot.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomoderatorGameBot.Singletons
{
    public static class GameUtils
    {

        public static async Task<GameUser> AddNewUser(GameUser newUser)
        {
            await using var dbContext = new GameDbContext();
            dbContext.Add(newUser);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            await dbContext.GameUsers.ToListAsync().ConfigureAwait(false);
            return newUser;
        }

        public static async Task<List<GameUser>> GetGameUsers()
        {
            await using var dbContext = new GameDbContext();
            var users = await dbContext.GameUsers.ToListAsync().ConfigureAwait(false);
            return users;
        }

        public static async Task<GameUser> GetGameUser(ulong discordId)
        {
            await using var dbContext = new GameDbContext();
            var gameUser = await dbContext.GameUsers.FirstOrDefaultAsync(x => x.DiscordUserId == discordId);
            return gameUser;
        }



    }
}
