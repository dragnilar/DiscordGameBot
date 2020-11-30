using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.DbContexts;
using AutomoderatorGameBot.BackEnd.Extensions;
using AutomoderatorGameBot.BackEnd.Models;
using CsvHelper;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace AutomoderatorGameBot.Modules
{
    public class CopyPastaModule : BaseCommandModule
    {
        public List<CopyPasta> CopyPastas => LoadCopyPastas();
        public List<VideoPasta> VideoPastas => LoadVideoPastas();

        private List<CopyPasta> LoadCopyPastas()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\CopyPasta.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<CopyPasta>().ToList();
        }

        private List<VideoPasta> LoadVideoPastas()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\Videos.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<VideoPasta>().ToList();
        }

        public async Task<bool> ProcessCopyPastas(MessageCreateEventArgs e)
        {
            var copyPasta = CopyPastas.FirstOrDefault(x => x.Command == e.Message.Content.ToLower());
            if (copyPasta == null) return false;
            if (ShutUp(copyPasta.ShutUp)) return true;
            if (!string.IsNullOrWhiteSpace(copyPasta.OptionalPicture))
            {
                switch (copyPasta.Pasta.ToLower())
                {
                    case "no text":
                        await e.Message.RespondWithFileAsync(
                            Path.Combine(Environment.CurrentDirectory, copyPasta.OptionalPicture), string.Empty);
                        break;
                    default:
                        await e.Message.RespondWithFileAsync(
                            Path.Combine(Environment.CurrentDirectory, copyPasta.OptionalPicture), copyPasta.Pasta);
                        break;
                }
            }
            else
            {
                if (copyPasta.Pasta.Length > 2000)
                {
                    var copyPastaArray = copyPasta.Pasta.SplitIntoChunks(2000);
                    foreach (var pastaPart in copyPastaArray) await e.Message.RespondAsync(pastaPart);
                }
                else
                {
                    await e.Message.RespondAsync(copyPasta.Pasta);
                }
            }


            return true;
        }
        
        private bool ShutUp(bool usedShutUp)
        {
            using var db = new GameDbContext();
            var config = db.BotConfigs.FirstOrDefault();
            if (config == null) return false;
            var shutUpLastUsedSeconds = (DateTime.Now - config.ShutUpLastUsed).TotalSeconds;
            if (usedShutUp)
            {
                if (shutUpLastUsedSeconds < config.ShutUpDuration && config.ShutUpEnabled) return true;

                if (shutUpLastUsedSeconds >= config.ShutUpDuration && config.ShutUpEnabled)
                {
                    config.ShutUpEnabled = false;
                    db.SaveChanges();
                    return false;
                }

                if (config.ShutUpEnabled) return false;
                config.ShutUpLastUsed = DateTime.Now;
                config.ShutUpEnabled = true;
                db.SaveChanges();
                return true;
            }

            if (!config.ShutUpEnabled)
                return false;
            if (shutUpLastUsedSeconds < config.ShutUpDuration && config.ShutUpEnabled)
                return true;

            if (shutUpLastUsedSeconds >= config.ShutUpDuration && config.ShutUpEnabled)
            {
                config.ShutUpEnabled = false;
                db.SaveChanges();
                return false;
            }

            return false;
        }

        [Command("CancelShutUp")]
        [Description("Cancels a shut up so the bot will respond to copy pastas again.")]
        public async Task CancelShutUp(CommandContext ctx)
        {
            await using var db = new GameDbContext();
            var config = db.BotConfigs.FirstOrDefault();
            if (config == null) return;
            var shutUpLastUsed = (DateTime.Now - config.ShutUpLastUsed).TotalSeconds;
            if (config.ShutUpEnabled)
            {
                config.ShutUpEnabled = false;
                await db.SaveChangesAsync();
                await ctx.RespondAsync("The shut ups been cancelled. I CAN SING AGAIN!");
            }
            else
            {
                await ctx.RespondAsync("Nobody has told me to STFU.");
            }
        }

        [Command("copypasta")]
        [Description(
            "Lists all of the copy pasta key word triggers that you can accidentally or purposefully trigger.")]
        public async Task CopyPastaHelp(CommandContext ctx)
        {
            var builder = new StringBuilder();
            var copyPastas = CopyPastas;
            for (var i = 0; i < copyPastas.Count; i++)
            {
                if (!copyPastas[i].VisibleInHelp) continue;
                builder.Append(copyPastas[i].Command);
                if (i != copyPastas.Count - 1) builder.Append(", ");
            }

            var embed = new DiscordEmbedBuilder
            {
                Title = "Copy Pastas",
                Color = new Optional<DiscordColor>(DiscordColor.DarkGreen),
                Description = "These are the available shitty copy pasta triggers that you may trigger:"
            };
            embed.AddField("Key Words", builder.ToString());
            await ctx.RespondAsync(null, false, embed.Build());
        }
    }
}