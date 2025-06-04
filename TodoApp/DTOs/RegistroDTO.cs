using System.ComponentModel.DataAnnotations;
using TodoApp.Models;

namespace TodoApp.DTOs
{
    public class RegistroDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 255 caracteres")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 255 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public string Correo { get; set; } = string.Empty;

        public UserRole? Rol { get; set; }
    }
} 