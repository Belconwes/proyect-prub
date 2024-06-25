using Microsoft.AspNetCore.Mvc;
using Proyecto_prueba.Models;
using Proyecto_prueba.ViewsModels;
using Microsoft.EntityFrameworkCore;
using Proyecto_prueba.Utilities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Proyecto_prueba.Controllers
{
    public class AccesController : Controller
    {
        private readonly PruebafContext _pruebafContext;
        public AccesController(PruebafContext pruebafcontext)
        {
            _pruebafContext = pruebafcontext;
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(UsuarioVM modelo)
        {
            if (modelo.Contraseña != modelo.Confirmar_contraseña)
            {
                ViewData["Message"] = "The passwords do not match";
                return View();
            }
            string hashedPassword = PasswordHasher.HashPassword(modelo.Contraseña);
            Usuario usuario = new Usuario()
            {
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Email = modelo.Email,
                Contraseña = hashedPassword,
                  Rol = new string[] { "User" }
            };
            await _pruebafContext.Usuarios.AddAsync(usuario);
            await _pruebafContext.SaveChangesAsync();

            if (usuario.IdUser != 0)
            {
                return RedirectToAction("Login", "Acces");

            }
            ViewData["Message"] = "No se pudo registrar al usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public PruebafContext GetPruebafContext()
        {
            return _pruebafContext;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuario? usuario_found = await _pruebafContext.Usuarios
                                    .Where(u =>
                                     u.Email == modelo.Email
                                     ).FirstOrDefaultAsync();
            if (usuario_found == null || !PasswordHasher.VerifyPassword(modelo.Contraseña, usuario_found.Contraseña))
            {
                ViewData["Message"] = "No se encontro el usuario solicitado, por favor revise los campos a rellenar";
                return View();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,usuario_found.Nombre)

            };
            foreach (var role in usuario_found.Rol)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties

                );
            return RedirectToAction("Index", "Home");

        }

    }
}
