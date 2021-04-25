using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.Models;
using CsvHelper;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;

namespace AutomoderatorGameBot.Modules
{
    public class ReactionModule
    {

        private static IEnumerable<Reaction> GetReactions()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\Reactions.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<Reaction>().ToList();
        }

        public async Task ProcessReactions(MessageCreateEventArgs e, DiscordClient client)
        {
            var reactions = GetReactions().Where
                (x => e.Message.Content.ToLower().Contains(x.ReactKeyword)).ToList();
            if (!reactions.Any()) return;
            var reactionCount = 0;
            foreach (var emoji in reactions.Select(
                reaction => DiscordEmoji.FromName(client, reaction.ReactionEmojiCode)))
            {
                client.Logger.Log(LogLevel.Information, $"Sending reaction Emoji: {emoji.Name}");
                await e.Message.CreateReactionAsync(emoji);
                reactionCount++;
                if (reactionCount == 20) break; //Discord caps reactions at 20, so stop if there's 20.
            }
        }
    }
}