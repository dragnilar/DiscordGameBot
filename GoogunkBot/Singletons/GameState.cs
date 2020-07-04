using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogunkBot.BackEnd.DbContexts;
using GoogunkBot.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace GoogunkBot.Singletons
{
    public static class GameState
    {
        public static List<GameUser> GameUsers;

        public static void Initialize()
        {
            using (var dbContext = new GameDbContext())
            {
                GameUsers = dbContext.GameUsers.ToList();
            }
        }

        public static async Task<GameUser> AddNewUser(GameUser newUser)
        {
            await using var dbContext = new GameDbContext();
            dbContext.Add(newUser);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            GameUsers = await dbContext.GameUsers.ToListAsync().ConfigureAwait(false);
            return newUser;
        }

        public static async Task<List<GameUser>> GetAndResyncUsers()
        {
            await using var dbContext = new GameDbContext();
            GameUsers = await dbContext.GameUsers.ToListAsync().ConfigureAwait(false);
            return GameUsers;
        }
    }
}
