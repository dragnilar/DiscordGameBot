using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            draftedUser = GameState.GameUsers.FirstOrDefault(x => x.DiscordUserId == discordId);

            if (draftedUser != null && draftedUser.IsDrafted)
            {
                await ctx.RespondAsync(
                    $"You've already been drafted and sent to Nam with the Poopballs on {draftedUser.DateTimeAdded:f}");
            }
            else
            {
                await GameState.AddNewUser(new GameUser
                    {DateTimeAdded = DateTime.Now, DiscordUserId = discordId, IsDrafted = true}).ConfigureAwait(false);
                await ctx.RespondAsync(
                    $"You've just been drafted and sent to Nam with the Poopballs, {ctx.Member.DisplayName}! Sucks to be you!");
            }
        }

        [Command("drafted")]
        [Description("Get List of everyone who has been drafted with the Poop Balls and sent to Nam")]
        public async Task Drafted(CommandContext ctx)
        {
            var players = GameState.GameUsers;
            var embed = new DiscordEmbedBuilder();
            embed.Title = "Drafted Soldiers";
            embed.Description = "All these maggots got drafted and sent to Nam with the Poop Balls!";
            var members = ctx.Guild.Members;
            foreach (var player in players)
            {
                var member = members.Where(x => x.Key == player.DiscordUserId).FirstOrDefault();
                if (member.Value != null)
                {
                    embed.AddField($"{member.Value.Username}", $"Draft Date: {player.DateTimeAdded:f}");
                }

            }


            await ctx.RespondAsync("", embed: embed);
        }
    }
}
