using System;
using System.Collections.Generic;
using System.Text;

namespace AutomoderatorGameBot.BackEnd.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
