using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        
        public DateTime FechaCreacion { get; set; }

        // Navegación
        public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
} 