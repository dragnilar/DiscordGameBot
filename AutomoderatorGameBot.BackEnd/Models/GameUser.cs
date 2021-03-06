﻿using System;
using System.Collections.Generic;

namespace AutomoderatorGameBot.BackEnd.Models
{
    public class GameUser
    {
        public int Id { get; set; }
        public ulong DiscordUserId { get; set; }
        public bool IsDrafted { get; set; }
        public long ShitCoins { get; set; }
        public int? CoolDownId { get; set; }
        public virtual CoolDown CoolDown { get; set; }
        public virtual List<Item> Items { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }
}