using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.Extensions;
using AutomoderatorGameBot.BackEnd.Models;
using Bogus;
using CsvHelper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace AutomoderatorGameBot.Modules
{
    public class AmaModule
    {
        private static IEnumerable<AmaResponse> GetResponses()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\AmaStrings.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<AmaResponse>().ToList();
        }

        public async Task ProcessReactions(MessageCreateEventArgs e)
        {
            var reactions = GetResponses().ToList();
            if (!reactions.Any()) return;
            var faker = new Faker();
            var responseBuilder = new StringBuilder();
            for (var i = 0; i < 4; i++)
            {
                responseBuilder.Append(faker.Random.ListItem(reactions).Response + "\n");
            }
            await e.Message.RespondAsync(responseBuilder.ToString());
        }
    }
}