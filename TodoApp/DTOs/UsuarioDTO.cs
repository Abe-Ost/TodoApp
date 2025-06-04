using System;
using TodoApp.Models;

namespace TodoApp.DTOs
{
    public class UsuarioResponseDTO
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public UserRole Rol { get; set; }
        public int TotalTareas { get; set; }
        public int TareasPendientes { get; set; }
        public int TareasEnProgreso { get; set; }
        public int TareasCompletadas { get; set; }
    }
} 