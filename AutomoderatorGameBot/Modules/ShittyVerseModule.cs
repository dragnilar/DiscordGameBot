using System.Threading.Tasks;
using AutomoderatorGameBot.Singletons;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

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
                    "You need to get drafted and go to Nam with the privates before you can do that buddy.");
            }
            else
            {
                var embed = new DiscordEmbedBuilder();
                embed.Title = $"{ctx.Member.Nickname}'s Balance";
                embed.Description = "This is your current balance of shit coins.";
                embed.AddField("Shit Coins", $"{player.ShitCoins:C2}");

                await ctx.RespondAsync("", embed: embed);
            }
        }
    }
}