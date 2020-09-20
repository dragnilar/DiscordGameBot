using System.ComponentModel.DataAnnotations.Schema;

namespace AutomoderatorGameBot.BackEnd.Models
{
    [NotMapped]
    public class AmaResponse
    {
        public string Response { get; set; }
    }
}