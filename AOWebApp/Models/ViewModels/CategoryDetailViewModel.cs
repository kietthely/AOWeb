using System.ComponentModel.DataAnnotations;

namespace AOWebApp.Models.ViewModels
{
    public class CategoryDetailViewModel
    {
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ItemCount { get; set; }
    }
}
