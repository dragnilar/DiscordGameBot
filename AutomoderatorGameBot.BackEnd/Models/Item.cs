using System;
using System.Collections.Generic;
using System.Text;

namespace AutomoderatorGameBot.BackEnd.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public long SellPrice { get; set; }
        public int? GameUserId { get; set; }
        public int? ShopId { get; set; }
        public virtual GameUser GameUser { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
