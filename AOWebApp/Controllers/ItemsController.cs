using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.Models.ViewModels;
using AOWebApp.Helpers;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public ItemsController(AmazonOrdersContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(ItemSearchViewModel vm, string SortOrder, int? PageNumber)
        {

            if (!PageNumber.HasValue) { PageNumber = 1; }
            ViewBag.SortOrder = SortOrder;
            ViewBag.PageNumber = PageNumber;    

            #region CategoriesQuery
            var categories = _context.ItemCategories
            .Where(i => i.ParentCategoryId == null)
            .OrderBy(i => i.CategoryName)
            .Select(i => new
            {
                CategoryId = i.CategoryId,
                CategoryName = i.CategoryName
            })
            .ToList();


            vm.CategoryList = new SelectList(categories, nameof(ItemCategory.CategoryId), nameof(ItemCategory.CategoryName));
            #endregion


            #region ItemQuery

            var query = _context.Items.Include(i => i.Category).OrderBy(i => i.ItemName).AsQueryable();
            switch (SortOrder) 
            {
                case "name_desc":
                    query = query.OrderByDescending(p => p.ItemName);
                    break;
                case "price_asc":
                    query = query.OrderBy(p => p.ItemCost);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.ItemCost);
                    break;
                default:
                    query = query.OrderBy(p => p.ItemName);
                    break;
            }
            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                query = query.Where(i => i.ItemName.Contains(vm.SearchText));
            }
            if (vm.CategoryId > 0)
            {
                query = query.Where(i => i.CategoryId == vm.CategoryId);
            }

            int PageSize = 3;
            vm.ItemList = await PaginatedList<Item_ItemDetail>.CreateAsync(
                query.Select(i => new Item_ItemDetail
                {
                    TheItem = i,
                    ReviewCount = (i.Reviews != null ? i.Reviews.Count : 0),
                    AverageRating = (i.Reviews != null && i.Reviews.Count() > 0 ? i.Reviews.Select(r => r.Rating ).Average() : 0)

                }
                ).AsNoTracking(), PageNumber ?? 1, PageSize);
           
           


            #endregion
            return View(vm);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'AmazonOrdersContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
