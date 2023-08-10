using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.Models.ViewModels
{
    public class Item_ItemDetail
    {
        public Item TheItem { get; set; }
        public int ReviewCount{ get; set; }
        public double AverageRating { get; set; }

    }
}
