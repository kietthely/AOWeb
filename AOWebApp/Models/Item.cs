using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public int ItemId { get; set; }
        [Required(ErrorMessage ="Please provide the item name")]
        [Display(Name = "Item Name")]
        [StringLength(150, ErrorMessage ="The item name must be less than 150 characters")]
        public string ItemName { get; set; } = null!;
        [Required(ErrorMessage = "Please provide the item description")]
        [Display(Name = "Item Description")]

        public string ItemDescription { get; set; } = null!;


        [Required(ErrorMessage = "Please provide the cost of this item")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ItemCost { get; set; }
        [Required(ErrorMessage ="Please provide the item image url")]
        [DataType(DataType.ImageUrl)]
        public string ItemImage { get; set; } = null!;
        [DataType(DataType.PostalCode)]
        public int? CategoryId { get; set; }

        public virtual ItemCategory? Category { get; set; }
        public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; }
        public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
