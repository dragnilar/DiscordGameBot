using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutomoderatorGameBot.BackEnd.Models;
using CsvHelper;

namespace AutomoderatorGameBot.Modules
{
    public class ReactionModule
    {
        public List<Reaction> GetReactions()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\Reactions.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<Reaction>().ToList();
        }
    }
}