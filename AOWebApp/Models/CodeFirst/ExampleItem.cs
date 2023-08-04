using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models.CodeFirst
{
    public class ExampleItem
    {
        [Key]
        public int itemID { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        [StringLength(100, ErrorMessage = "The item name must be less than 100 characters")]
        public string itemName { get; set; } = null!;


        [Required(ErrorMessage ="You must provide an item price")]
        [Range(1, 10000, ErrorMessage ="The {0} must be between {1} and {2}")]
        [Display(Name ="Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal itemPrice { get; set; }
    }
}
