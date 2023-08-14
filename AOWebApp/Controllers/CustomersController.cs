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

namespace AOWebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public CustomersController(AmazonOrdersContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(CustomerSearchViewModel vm)
        {
            #region SuburbSearch
            var suburbQuery = _context.Addresses
                .Select(a => a.Suburb)
                .Distinct()
                .OrderBy(a => a)
                .ToList();
            ViewBag.SuburbList = new SelectList(suburbQuery, vm.Suburb);
            #endregion
            
            List<Customer> CustomerList = new List<Customer>();
            #region CustomerSearch


            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                var query = _context.Customers
                    .Include(c => c.Address)
                    .Where(c => vm.SearchText.Split().Length > 1
                    ? c.FirstName.Equals(vm.SearchText.Split()[0]) && c.LastName.Equals(vm.SearchText.Split()[1])
                    : c.FirstName.StartsWith(vm.SearchText) || c.LastName.StartsWith(vm.SearchText));
                if (!string.IsNullOrWhiteSpace(vm.Suburb))
                {
                    query = query.Where(c => c.Address.Suburb == vm.Suburb);

                }
                query = query.OrderBy(c => vm.SearchText.Split().Length > 1
                        ? c.FirstName.StartsWith(vm.SearchText.Split()[0])
                        : c.FirstName.StartsWith(vm.SearchText))
                    .ThenBy(c => vm.SearchText.Split().Length > 1
                        ? c.LastName.StartsWith(vm.SearchText.Split()[1])
                        : c.LastName.StartsWith(vm.SearchText));
                vm.CustomerList = await query.ToListAsync();
            }
            else {
                var query = _context.Customers.Include(c => c.Address);
                vm.CustomerList = await query.ToListAsync();
            }

            //var query = _context.Customers.Include(c => c.Address).
            //    Where(c => c.FirstName.StartsWith(vm.SearchText) && c.LastName.StartsWith(vm.SearchText))
            //    .OrderBy(c => c.FirstName.StartsWith(vm.SearchText))
            //    .ThenBy(c => c.LastName.StartsWith(vm.SearchText));

            //if (!string.IsNullOrEmpty(vm.Suburb))

            //{
            //    query = _context.Customers.Include(c => c.Address).
            //    Where(c => c.FirstName.StartsWith(vm.SearchText) && c.LastName.StartsWith(vm.SearchText))
            //    .Where(c => c.Address.Suburb == vm.Suburb)
            //    .OrderBy(c => c.FirstName.StartsWith(vm.SearchText))
            //    .ThenBy(c => c.LastName.StartsWith(vm.SearchText));
            //}
            //vm.CustomerList = await query.ToListAsync();


            #endregion

            return View(vm);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,MainPhoneNumber,SecondaryPhoneNumber,AddressId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,MainPhoneNumber,SecondaryPhoneNumber,AddressId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'AmazonOrdersContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
