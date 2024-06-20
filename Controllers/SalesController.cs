using Microsoft.AspNetCore.Mvc;
using bestbrigh.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using bestbrigh.data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bestbrigh.Controllers
{
    public class SalesController : Controller
    {
      private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
     


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            IQueryable<Sale> salesQuery = _context.Sales
                .Include(s => s.Product)
                .Include(s => s.User);

            if (role == "Salesperson")
            {
                salesQuery = salesQuery.Where(s => s.UserId.ToString() == userId);
            }

            var sales = await salesQuery.ToListAsync();
            return View(sales);
        }
        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View();
        }

        // POST: Sales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,ProductId,UserId,Quantity,SalePrice,SaleDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            ViewBag.Products = _context.Products.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(sale);
        }

        // POST: Sales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,ProductId,UserId,Quantity,SalePrice,SaleDate")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.SaleId == id);
        }
    }
}
