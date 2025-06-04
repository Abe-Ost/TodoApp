using System.ComponentModel.DataAnnotations;

namespace TodoApp.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasena { get; set; } = string.Empty;
    }
} 