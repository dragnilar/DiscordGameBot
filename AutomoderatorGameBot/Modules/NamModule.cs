﻿using System;
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
    public class NamModule : BaseCommandModule
    {
        [Command("maggots")]
        [Description("Get Drafted with the maggots and get sent to Nam")]
        public async Task Maggots(CommandContext ctx)
        {
            GameUser draftedUser = null;
            var discordId = ctx.Member.Id;
            draftedUser = await GameUtils.GetGameUser(discordId).ConfigureAwait(false);

            if (draftedUser != null && draftedUser.IsDrafted)
            {
                await ctx.RespondAsync(
                    $"You've already been drafted and sent to Nam with the other maggots on {draftedUser.DateTimeAdded:f}");
            }
            else
            {
                await GameUtils.AddNewUser(new GameUser
                    {DateTimeAdded = DateTime.Now, DiscordUserId = discordId, IsDrafted = true}).ConfigureAwait(false);
                await ctx.RespondAsync(
                    $"You've just been drafted and sent to Nam with the other maggots, {ctx.Member.DisplayName}! Sucks to be you!");
            }
        }

        [Command("drafted")]
        [Description("Get List of everyone who has been drafted with the maggots and sent to Nam")]
        public async Task Drafted(CommandContext ctx)
        {
            var players = GameUtils.GetGameUsers().Result;
            var embed = new DiscordEmbedBuilder
            {
                Title = "Drafted Soldiers",
                Description = "All these maggots got drafted and sent to Nam with the maggots!"
            };
            var members = ctx.Guild.Members;
            foreach (var player in players)
            {
                var member = members.FirstOrDefault(x => x.Key == player.DiscordUserId);
                if (member.Value != null)
                    embed.AddField($"{member.Value.Username}", $"Draft Date: {player.DateTimeAdded:f}");
            }


            await ctx.RespondAsync("", embed: embed);
        }

        [Command("mine")]
        [Description("Make a run through the mine field to amuse the Sarge without getting blown up.")]
        public async Task MineFields(CommandContext ctx)
        {
            var player = await GameUtils.GetGameUser(ctx.Member.Id).ConfigureAwait(false);
            if (player == null)
            {
                await ctx.RespondAsync(
                    "You need to get drafted and go to Nam with the maggots before you can do that, shit head.");
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

                dbUser.CoolDown ??= new CoolDown();

                var secondsSinceLastMine = (DateTime.Now - dbUser.CoolDown.MineLastUsed).TotalSeconds;
                if (secondsSinceLastMine < 30)
                {
                    await ctx.RespondAsync(
                        $"You just went out into the mine fields not too long ago shit head, give yourself {30 - secondsSinceLastMine:N2} seconds to get bandaged up!");
                    return;
                }

                dbUser.CoolDown.MineLastUsed = DateTime.Now;
                var faker = new Faker();
                var roll = faker.Random.Int(1, 100);
                if (roll <= 20)
                {
                    var loss = faker.Random.Long(100, 500) * roll;
                    if (dbUser.ShitCoins < loss)
                        dbUser.ShitCoins = 0;
                    else
                        dbUser.ShitCoins -= loss;

                    await dbContext.SaveChangesAsync();
                    await ctx.RespondAsync(
                        $"You stepped on a land mine and died! You lost {loss} Shit Coins.");
                }
                else
                {
                    var gain = faker.Random.Long(10, 50) * roll;
                    dbUser.ShitCoins += gain;
                    await dbContext.SaveChangesAsync();
                    await ctx.RespondAsync(
                        $"You and the maggots managed to make it through the mine field and killed some gooks! You got ${gain} Shit Coins!");
                }
            }
        }

        [Command("sad")]
        [Description(
            "Run a Search and Destroy Mission to get some Gooks and maybe also experience a horrible drug trip or worse...")]
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