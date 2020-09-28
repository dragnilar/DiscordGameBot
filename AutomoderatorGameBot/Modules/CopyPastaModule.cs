﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (!string.IsNullOrWhiteSpace(copyPasta.OptionalPicture))
            {
                await e.Message.RespondWithFileAsync(
                    Path.Combine(Environment.CurrentDirectory, copyPasta.OptionalPicture), copyPasta.Pasta);
            }
            else
            {
                await e.Message.RespondAsync(copyPasta.Pasta);
            }
            return true;

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
                if (i != copyPastas.Count - 1)
                {
                    builder.Append(", ");
                }
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