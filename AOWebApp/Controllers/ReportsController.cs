using AOWebApp.Data;
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
            var yearList = _context.CustomerOrders.Select(co => co.OrderDate.Year).OrderByDescending(co=> co).Distinct().ToList();



            return View("AnnualSalesReport", new SelectList(yearList));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int Year)
        {
            if (Year > 0) {
                var order = _context.ItemsInOrders.Where(iio => iio.OrderNumberNavigation.OrderDate.Year == Year)
                .GroupBy(iio => new { iio.OrderNumberNavigation.OrderDate.Year, iio.OrderNumberNavigation.OrderDate.Month })
                .Select(obj => new
                {
                    year = obj.Key.Year,
                    month = obj.Key.Month,
                    monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(obj.Key.Month),
                    totalItems = obj.Sum(iio => iio.NumberOf),
                    totalCost = obj.Sum(iio => iio.TotalItemCost)

                }).OrderBy(d => d.month);
                // OR
                //var summaryQuery = _context.ItemsInOrders.Where(iio => iio.OrderNumberNavigation.OrderDate.Year == Year)
                //.GroupBy(iio => new { iio.OrderNumberNavigation.OrderDate.Year, iio.OrderNumberNavigation.OrderDate.Month })
                //.Select(obj => new
                //{
                //    year = obj.Key.Year,
                //    month = obj.Key.Month,
                //    monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(obj.Key.Month),
                //    totalItems = obj.Sum(iio => iio.NumberOf),
                //    totalCost = obj.Sum(iio => iio.TotalItemCost)

                //}).OrderBy(d => d.month);
                //var summary = summaryQuery.Select(os => new
                //{
                //    os.year, os.month, monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(os.month),
                //    os.totalItems, os.totalCost
                //});
                return Json(order);
            } else {
                return BadRequest();
            }
            
        }
    }
}
