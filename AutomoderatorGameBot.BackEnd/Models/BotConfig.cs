using System;

namespace AutomoderatorGameBot.BackEnd.Models
{
    public class BotConfig
    {
        public int Id { get; set; }
        public DateTime ShutUpLastUsed { get; set; }
        public int ShutUpDuration { get; set; }
        public bool ShutUpEnabled { get; set; }
    }
}