using Microsoft.AspNetCore.Mvc;
using Proyecto_prueba.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace Proyecto_prueba.Controllers
{
    public class HomeController : Controller
    {
        private readonly PruebafContext _context;

        public HomeController(PruebafContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            var productos = await _context.Productos.ToListAsync();
            return View(productos);
        }
        [Authorize(policy: "AdminOnly")]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Cerrar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult admin ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
