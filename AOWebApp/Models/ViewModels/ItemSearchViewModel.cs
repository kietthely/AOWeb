using Microsoft.AspNetCore.Mvc.Rendering;
using AOWebApp.Models;
using AOWebApp.Helpers;

namespace AOWebApp.Models.ViewModels
{
    public class ItemSearchViewModel
    {

        public string? SearchText { get; set; }
        public int? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
        public PaginatedList<Item_ItemDetail>? ItemList { get; set; }

    }
}
