using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutomoderatorGameBot.BackEnd.Models;
using Bogus;
using CsvHelper;

namespace AutomoderatorGameBot.Modules
{
    public class CopyPastaModule
    {
        private readonly Faker Faker = new Faker();
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

        public string GetWaffle()
        {
            return Faker.WaffleText(Faker.Random.Int(1, 3));
        }
    }
}