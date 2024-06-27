using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_prueba.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace Proyecto_prueba.Controllers
{
    public class CarritoController : Controller
    {
        private readonly PruebafContext _context;

        public CarritoController(PruebafContext context)
        {
            _context = context;
        }

        //Index del carrito
        // GET: Carrito
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var pedidos = await _context.Pedidos
                .Where(p => p.UsuarioId.ToString() == userId)
                .Include(p => p.Carritos)
                .ThenInclude(c => c.Producto)
                .ToListAsync();

            var carrito = pedidos.SelectMany(p => p.Carritos).ToList();

            return View(carrito);
        }

        // POST: Carrito/AddToCarrito
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCarrito(int productoId, int cantidad)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine("Name user: " + userId);

            if (userId != null && int.TryParse(userId, out int userIdInt))
            {
                Console.WriteLine("Pasa por aca 1.1");
                // Lógica para usuarios autenticados
                var pedido = await _context.Pedidos
                    .FirstOrDefaultAsync(p => p.UsuarioId == userIdInt);

                if (pedido == null)
                {
                    pedido = new Pedido
                    {
                        UsuarioId = userIdInt,
                        Fecha = DateOnly.FromDateTime(DateTime.Now),
                    };

                    _context.Pedidos.Add(pedido);
                    await _context.SaveChangesAsync();
                }

                var carrito = new Carrito
                {
                    PedidoId = pedido.PedidoId,
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    Estado = false
                };

                _context.Carritos.Add(carrito);
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Pasa por aca 1.2");
                // Lógica para usuarios no autenticados usando cookies
                AddToCarritoInCookies(productoId, cantidad);
            }

            return RedirectToAction("Index");
        }

        // POST: Carrito/RemoveFromCarrito
        [HttpPost]
        public async Task<IActionResult> RemoveFromCarrito(int carritoId)
        {
            var carrito = await _context.Carritos.FindAsync(carritoId);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        private void AddToCarritoInCookies(int productoId, int cantidad)
        {
            Console.WriteLine("se desvio y sigue por aca 1.3");
            var carrito = GetCarritoFromCookies();

            var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);
            if (item != null)
            {
                Console.WriteLine("esta por aca 1.4");
                item.Cantidad += cantidad;
            }
            else
            {
                Console.WriteLine("esta por aca 1.5");
                carrito.Add(new Carrito
                {
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    Estado = false,
                    Producto = _context.Productos.Find(productoId)  // Supone que el producto existe en la base de datos
                });
            }

            SaveCarritoToCookies(carrito);
        }

        private List<Carrito> GetCarritoFromCookies()
        {
            Console.WriteLine("esta por aca 1.6");
            var carritoCookie = Request.Cookies["Carrito"];
            if (carritoCookie == null)
            {
                return new List<Carrito>();
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<Carrito>>(carritoCookie);
        }

        private void SaveCarritoToCookies(List<Carrito> carrito)
        {
            Console.WriteLine("esta por aca 1.7");
            var options = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };

            var carritoJson = System.Text.Json.JsonSerializer.Serialize(carrito);
            Response.Cookies.Append("Carrito", carritoJson, options);
        }





    }
}
