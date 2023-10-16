using AOWebApp.Data;
using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AOWebApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AmazonOrdersContext _context;

        public ReportsController(AmazonOrdersContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var yearList = _context.CustomerOrders.Select(co => co.OrderDate.Year).Distinct().OrderByDescending(co=> co).ToList();



            return View("AnnualSalesReport", new SelectList(yearList));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int Year)
        {

            if (Year > 0) {
                var order = _context.ItemsInOrders
                    .Where(iio => iio.OrderNumberNavigation.OrderDate.Year == Year)
                .GroupBy(iio => new { iio.OrderNumberNavigation.OrderDate.Year, iio.OrderNumberNavigation.OrderDate.Month })
                .Select(group => new
                {
                    year = group.Key.Year,
                    monthNo = group.Key.Month,
                    monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month),
                    totalItems = group.Sum(iio => iio.NumberOf),
                    totalCost = group.Sum(iio => iio.TotalItemCost)

                }).OrderBy(d => d.monthNo);
            
                return Json(order);
            } else {
                return BadRequest();
            }
            
        }
    }
}
