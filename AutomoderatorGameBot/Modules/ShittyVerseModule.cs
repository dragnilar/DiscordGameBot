using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.DbContexts;
using AutomoderatorGameBot.BackEnd.Models;
using AutomoderatorGameBot.Singletons;
using Bogus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace AutomoderatorGameBot.Modules
{
    public class ShittyVerseModule : BaseCommandModule
    {
        [Command("balance")]
        [Aliases("bal","shitcoins","sc")]
        public async Task Balance(CommandContext ctx)
        {
            var player = GameUtils.GetGameUser(ctx.Member.Id).Result;
            if (player == null)
            {
                await ctx.RespondAsync(
                    "You don't have a character in the shitty verse, so you obviously don't have any shit coins to see. 😝");
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

        [Command("start")]
        [Aliases("join","begin")]
        [Description("Start your journey in the shitty-verse, where all sorts of shitty shit...happens. 🤔")]
        public async Task StartGame(CommandContext ctx)
        {
            var discordId = ctx.Member.Id;
            var gameUser = await GameUtils.GetGameUser(discordId).ConfigureAwait(false);

            if (gameUser != null && gameUser.IsDrafted)
            {
                await ctx.RespondAsync(
                    $"You already created your character for the shitty verse on: {gameUser.DateTimeAdded:f}");
            }
            else
            {
                await GameUtils.AddNewUser(new GameUser
                    {DateTimeAdded = DateTime.Now, DiscordUserId = discordId, IsDrafted = true}).ConfigureAwait(false);
                await ctx.RespondAsync(
                    $"Congratulations {ctx.Member.DisplayName}! Your character for the shitty-verse has been created. Cool..? 😎 ");
            }
        }

        [Command("playerlist")]
        [Aliases("pl","plist")]
        [Description("Get List of everyone who has been drafted with the maggots and sent to Nam")]
        public async Task GetPlayerList(CommandContext ctx)
        {
            var players = GameUtils.GetGameUsers().Result;
            var embed = new DiscordEmbedBuilder
            {
                Title = "List Of Shitty Verse Players",
                Description = "💩 These are all of the people who were cool enough to make characters in the shitty verse. 💩"
            };
            var members = ctx.Guild.Members;
            foreach (var player in players)
            {
                var member = members.FirstOrDefault(x => x.Key == player.DiscordUserId);
                if (member.Value != null)
                    embed.AddField($"{member.Value.Username}", $"Create Date: {player.DateTimeAdded:f}");
            }


            await ctx.RespondAsync("", embed: embed);
        }

        [Command("explore")]
        [Description(
            "Go explore somewhere in the shitty verse to look for shit coins, shitty treasure or meet shitty people or just... do something shitty, ya know? 😈")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task SearchAndDestroy(CommandContext ctx)
        {
            var player = await GameUtils.GetGameUser(ctx.Member.Id).ConfigureAwait(false);
            if (player == null)
            {
                await ctx.RespondAsync(
                    "You need to get drafted and go to Nam with the other maggots before you can do that, shit head.");
            }
            else
            {
                await using var dbContext = new GameDbContext();
                var dbUser = dbContext.GameUsers.FirstOrDefault(x => x.Id == player.Id);
                if (dbUser == null)
                {
                    await ctx.RespondAsync(
                        "You need to get drafted and go to Nam with the maggots before you can do that, shit head.");
                    return;
                }

                var faker = new Faker();
                IList<MiniGameChoice> options =
                    dbContext.MiniGameChoices.Where(x => x.MiniGameName == "SearchAndDestroy").ToList();
                options = faker.Random.ListItems(options, 3);
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Search And Destroy!",
                    Description = $"Pick a search and destroy mission, {ctx.Member.DisplayName}!"
                };
                var choiceNumber = 1;
                var optionsBuilder = new StringBuilder();
                foreach (var option in options)
                {
                    optionsBuilder.Append(" *" + option.ChoiceName + "* ");
                    choiceNumber++;
                }

                embed.AddField("Missions", optionsBuilder.ToString(), true);
                await ctx.RespondAsync("", embed: embed);
                while (DateTime.Now < DateTime.Now.AddSeconds(120))
                {
                    var interactivity = ctx.Client.GetInteractivity();
                    var playerInput = await interactivity.WaitForMessageAsync(x => x.Author.Id == dbUser.DiscordUserId);
                    if (playerInput.Result == null) continue;
                    var lowerInput = playerInput.Result.Content.ToLower();
                    var choice = options.FirstOrDefault(x => x.ChoiceName == lowerInput);
                    if (choice == null)
                    {
                        await ctx.RespondAsync(
                            "That's not an option private!");
                        return;
                    }

                    var roll = faker.Random.Int(1, 100);
                    if (roll <= choice.FailResultChance)
                    {
                        var loss = 100 * roll;
                        if (dbUser.ShitCoins < loss)
                            dbUser.ShitCoins = 0;
                        else
                            dbUser.ShitCoins -= loss;
                        await dbContext.SaveChangesAsync();
                        await ctx.RespondAsync($"{choice.FailResultText} {loss:C2}");
                        return;
                    }

                    if (roll > choice.RegularResultChance)
                    {
                        dbUser.ShitCoins += choice.SpecialResultMoney;
                        await dbContext.SaveChangesAsync();
                        await ctx.RespondAsync(
                            $"{choice.SpecialResultText} {choice.SpecialResultMoney:C2}");
                    }
                    else
                    {
                        dbUser.ShitCoins += choice.RegularResultMoney;
                        await dbContext.SaveChangesAsync();
                        await ctx.RespondAsync(
                            $"{choice.RegularResultText} {choice.RegularResultMoney:C2}");
                    }

                    return;
                }

                await ctx.RespondAsync(
                    "Look shit head, don't waste Sarge's time, he doesn't like it when you sit on your ass and do nothing.");
            }
        }
    }
}