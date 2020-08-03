using System;
using System.Collections.Generic;
using System.Text;

namespace GoogunkBot.BackEnd.Models
{
    public class GameUser
    {
        public int Id { get; set; }
        public ulong DiscordUserId { get; set; }
        public bool IsDrafted { get; set; }
        public long PoopBucks { get; set; } 
        public long ShitBucks { get; set; }
        public int? CoolDownId { get; set; }
        public virtual CoolDown CoolDown { get; set; }
        public virtual List<Item> Items { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }
}
