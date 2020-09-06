using System.ComponentModel.DataAnnotations.Schema;

namespace AutomoderatorGameBot.BackEnd.Models
{
    [NotMapped]
    public class Reaction
    {
        public string ReactKeyword { get; set; }
        public string ReactionEmojiCode { get; set; }
    }
}