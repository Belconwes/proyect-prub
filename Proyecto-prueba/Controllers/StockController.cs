using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_prueba.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_prueba.Controllers
{
    public class StockController : Controller
    {
        private readonly PruebafContext _context;

        public StockController(PruebafContext context)
        {
            _context = context;
        }
        // GET: StockController
        public ActionResult Index()
        {
            var stocks = _context.Stocks.Include(s => s.Producto).Include(s => s.Proveedor).ToList();
            return View(stocks);
        }

        // GET: StockController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockController/Create
        public ActionResult Create()
        {
            ViewBag.Productos = _context.Productos.ToList(); // Lista de productos para seleccionar
            ViewBag.Proveedores = _context.Proveedores.ToList(); // Lista de proveedores para seleccionar
            return View();
        }


        // POST: StockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Stocks.Add(stock);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Proveedores = _context.Proveedores.ToList();
            return View(stock);
        }

        // GET: StockController/Edit/5
        public ActionResult Edit(int id)
        {
            var stock = _context.Stocks.Include(s => s.Producto).Include(s => s.Proveedor).FirstOrDefault(s => s.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Proveedores = _context.Proveedores.ToList();
            return View(stock);
        }

        // POST: StockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Stock stock)
        {

            if (ModelState.IsValid)
            {
                _context.Stocks.Update(stock);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Proveedores = _context.Proveedores.ToList();
            return View(stock);
        }

        // GET: StockController/Delete/5
        public ActionResult Delete(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: StockController/Delete/5
    }
}
