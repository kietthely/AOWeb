using System;
using System.Collections.Generic;

namespace AOWebApp.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemMarkupHistories = new HashSet<ItemMarkupHistory>();
            ItemsInOrders = new HashSet<ItemsInOrder>();
            Reviews = new HashSet<Review>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public decimal ItemCost { get; set; }
        public string ItemImage { get; set; } = null!;
        public int? CategoryId { get; set; }

        public virtual ItemCategory? Category { get; set; }
        public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; }
        public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
