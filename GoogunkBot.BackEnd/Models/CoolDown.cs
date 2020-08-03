using System;

namespace GoogunkBot.BackEnd.Models
{
    public class CoolDown
    {
        public int Id { get; set; }
        public DateTime MineLastUsed { get; set; }
    }
}