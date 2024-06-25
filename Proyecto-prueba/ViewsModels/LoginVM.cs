using System.ComponentModel.DataAnnotations;

namespace Proyecto_prueba.ViewsModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Contraseña { get; set; }
    }
}
