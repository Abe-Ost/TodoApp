using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(255)]
        public string NombreUsuario { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string ContrasenaHash { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;
        
        public DateTime FechaCreacion { get; set; }

        [Required]
        public UserRole Rol { get; set; } = UserRole.User;

        // Navegación
        public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
} 