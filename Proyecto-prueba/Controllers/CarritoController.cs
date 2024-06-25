using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_prueba.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;


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
            Console.WriteLine("Pasa por aca 1");
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                Console.WriteLine("Pasa por aca 1.2");
                return Unauthorized();
            }
            else
            {
                // Usuario no autenticado
                Console.WriteLine("Usuario no autenticado.");

                // Aquí puedes implementar la lógica para agregar el producto al carrito
                // para usuarios no autenticados. Por ejemplo, podrías guardar la selección
                // en una sesión o en cookies.

                
            }

            // Obtener o crear un Pedido para el usuario
            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(p => p.UsuarioId.ToString() == userId);

            if (pedido == null)
            {
                Console.WriteLine("Pasa por aca 1.3");
                pedido = new Pedido
                {
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    //Estado = false  // Puedes ajustar el estado según tus necesidades
                };

                _context.Pedidos.Add(pedido);
                Console.WriteLine("Pasa por aca 2");
                await _context.SaveChangesAsync();
            }

            // Crear el Carrito
            var carrito = new Carrito
            {
                PedidoId = pedido.PedidoId,
                ProductoId = productoId,
                Cantidad = cantidad,
                Estado = false
            };

            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
            Console.WriteLine("Pasa por aca 3");

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





    }
}
