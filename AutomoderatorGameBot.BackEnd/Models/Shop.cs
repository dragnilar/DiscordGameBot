using System.Collections.Generic;

namespace AutomoderatorGameBot.BackEnd.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}