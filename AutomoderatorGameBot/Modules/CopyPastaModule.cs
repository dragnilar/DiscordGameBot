using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutomoderatorGameBot.BackEnd.Models;
using Bogus;
using CsvHelper;
using DSharpPlus.EventArgs;

namespace AutomoderatorGameBot.Modules
{
    public class CopyPastaModule
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
        
    }
}