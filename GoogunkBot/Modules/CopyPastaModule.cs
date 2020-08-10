using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Bogus;
using CsvHelper;
using GoogunkBot.BackEnd.Models;

namespace GoogunkBot.Modules
{
    public class CopyPastaModule
    {
        private Faker Faker = new Faker();
        private List<CopyPasta> _copyPastas;
        public IEnumerable<CopyPasta> CopyPastas => _copyPastas ?? LoadCopyPastas();

        private List<CopyPasta> LoadCopyPastas()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\CopyPasta.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<CopyPasta>().ToList();
        }

        public string GetWaffle()
        {
            return Faker.WaffleText(Faker.Random.Int(1, 3));
        }
    }
}