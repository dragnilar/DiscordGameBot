using System.ComponentModel.DataAnnotations.Schema;

namespace GoogunkBot.BackEnd.Models
{
    [NotMapped]
    public class AmaTransformer
    {
        public string InValue { get; set; }
        public string OutValue { get; set; }
    }
}