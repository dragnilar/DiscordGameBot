using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using GoogunkBot.BackEnd.Extensions;
using GoogunkBot.BackEnd.Models;

namespace GoogunkBot.Modules
{
    public class AmaModule
    {

        private List<AmaTransformer> LoadTransformers()
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Csv\\AmaStrings.csv"));
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<AmaTransformer>().ToList();
        }

        public string ConvertAmaString(string amaString)
        {
            var transformers = LoadTransformers();
            var returnString = amaString.ToLower();
            foreach (var amaTransformer in transformers.Where(amaTransformer => returnString.Contains(amaTransformer.InValue.ToLower())))
            {
                returnString = returnString.ReplaceFirst(amaTransformer.InValue.ToLower(), amaTransformer.OutValue);
                break;
            }

            returnString = ReplaceEndAMAString(returnString);

            return returnString;
        }

        private string ReplaceEndAMAString(string returnString)
        {
            var amaEndStrings = new HashSet<string>
            {
                "AMA","ama","AmA","AMa","aMA","aMa"
            };
            var matchingEnd = amaEndStrings.FirstOrDefault(returnString.Contains);
            returnString = returnString.ReplaceLastOccurrence(matchingEnd, "can I ask you anything?");
            return returnString;
        }
    }
}
