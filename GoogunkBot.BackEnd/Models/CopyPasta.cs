using System.ComponentModel.DataAnnotations.Schema;

namespace GoogunkBot.BackEnd.Models
{
    [NotMapped]
    public class CopyPasta
    {
        public string Command { get; set; }
        public string Pasta { get; set; }
    }
}