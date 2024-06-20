using bestbrigh.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bestbrigh.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public IActionResult Index()
        {
            return View();
        }

        // GET: Reports/DailySales
        public async Task<IActionResult> DailySales(DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Today;
            }

            var sales = await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.User)
                .Where(s => s.SaleDate == date.Value.Date)
                .ToListAsync();

            ViewBag.Date = date.Value.ToString("yyyy-MM-dd");

            return View(sales);
        }

        // GET: Reports/Inventory
        public async Task<IActionResult> Inventory()
        {
            var inventory = await _context.Products.ToListAsync();
            return View(inventory);
        }

        // GET: Reports/LowStock
        public async Task<IActionResult> LowStock()
        {
            var lowStockProducts = await _context.Products
                .Where(p => p.StockLevel < p.Threshold)
                .ToListAsync();

            return View(lowStockProducts);
        }
    }
}
