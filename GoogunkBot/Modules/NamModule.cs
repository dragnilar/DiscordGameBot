using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using GoogunkBot.BackEnd.DbContexts;
using GoogunkBot.BackEnd.Models;
using GoogunkBot.Singletons;

namespace GoogunkBot.Modules
{
    public class NamModule : BaseCommandModule
    {
        [Command("poopballs")]
        [Description("Get Drafted with the Poop Balls and get sent to Nam")]
        public async Task Poopballs(CommandContext ctx)
        {
            GameUser draftedUser = null;
            var discordId = ctx.Member.Id;
            draftedUser = await GameUtils.GetGameUser(discordId).ConfigureAwait(false);

            if (draftedUser != null && draftedUser.IsDrafted)
            {
                await ctx.RespondAsync(
                    $"You've already been drafted and sent to Nam with the Poopballs on {draftedUser.DateTimeAdded:f}");
            }
            else
            {
                await GameUtils.AddNewUser(new GameUser
                    {DateTimeAdded = DateTime.Now, DiscordUserId = discordId, IsDrafted = true}).ConfigureAwait(false);
                await ctx.RespondAsync(
                    $"You've just been drafted and sent to Nam with the Poopballs, {ctx.Member.DisplayName}! Sucks to be you!");
            }
        }

        [Command("drafted")]
        [Description("Get List of everyone who has been drafted with the Poop Balls and sent to Nam")]
        public async Task Drafted(CommandContext ctx)
        {
            var players = GameUtils.GetGameUsers().Result;
            var embed = new DiscordEmbedBuilder
            {
                Title = "Drafted Soldiers",
                Description = "All these maggots got drafted and sent to Nam with the Poop Balls!"
            };
            var members = ctx.Guild.Members;
            foreach (var player in players)
            {
                var member = members.FirstOrDefault(x => x.Key == player.DiscordUserId);
                if (member.Value != null)
                {
                    embed.AddField($"{member.Value.Username}", $"Draft Date: {player.DateTimeAdded:f}");
                }

            }


            await ctx.RespondAsync("", embed: embed);
        }

        [Command("mine")]
        [Description("Navigate the mine fields for glory and poop bucks.")]
        public async Task MineFields(CommandContext ctx)
        {

            var player = await GameUtils.GetGameUser(ctx.Member.Id).ConfigureAwait(false);
            if (player == null)
            {
                await ctx.RespondAsync(
                    "You need to get drafted and go to Nam with the Poop Balls before you can do that, shit head.");
            }
            else
            {
                await using var dbContext = new GameDbContext();
                var dbUser = dbContext.GameUsers.FirstOrDefault(x => x.Id == player.Id);
                if (dbUser == null)
                {     
                    await ctx.RespondAsync(
                        "You need to get drafted and go to Nam with the Poop Balls before you can do that, shit head.");
                    return;
                }
                
                var faker = new Faker();
                var roll = faker.Random.Int(1, 100);
                if (roll <= 12)
                {
                    var loss = faker.Random.Long(10, 50) * roll;
                    if (dbUser.PoopBucks < loss)
                    {
                        dbUser.PoopBucks = 0;
                    }
                    else
                    {
                        dbUser.PoopBucks -= loss;
                    }

                    await dbContext.SaveChangesAsync();
                    await ctx.RespondAsync(
                        $"You stepped on a land mine and died! You lost {loss} poop bucks.");
                }
                else
                {
                    var gain = faker.Random.Long(10, 50) * roll;
                    dbUser.PoopBucks += gain;
                    await dbContext.SaveChangesAsync();
                    await ctx.RespondAsync(
                        $"You and the poop balls managed to make it through the mine field and killed some gooks! You got ${gain} Poop Bucks!");
                }
            }
        }
    }
}
