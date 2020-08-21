using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomoderatorGameBot.Singletons;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using AutomoderatorGameBot.BackEnd.DbContexts;

namespace AutomoderatorGameBot.Modules
{
    public class ShittyVerseModule : BaseCommandModule
    {
        [Command("Bal")]
        public async Task Balance(CommandContext ctx)
        {
            var player = GameUtils.GetGameUser(ctx.Member.Id).Result;
            if (player == null)
            {
                await ctx.RespondAsync(
                    "You need to get drafted and go to Nam with the Poop Balls before you can do that, shit head.");
            }
            else
            {

                var embed = new DiscordEmbedBuilder();
                embed.Title = $"{ctx.Member.Nickname}'s Balance";
                embed.Description = "This is your current balance of poop bucks and shit bucks.";
                embed.AddField("Poop Bucks",$"{player.PoopBucks}");
                embed.AddField("Shit Bucks",$"{player.ShitBucks}");
                
                await ctx.RespondAsync("", embed: embed);
            }
        }
    }
}
