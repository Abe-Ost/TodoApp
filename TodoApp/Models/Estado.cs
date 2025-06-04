using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Estado
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string NombreEstado { get; set; } = string.Empty;

        // Navegación
        public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
} 