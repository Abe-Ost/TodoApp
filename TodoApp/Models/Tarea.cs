using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Tarea
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        
        public int? CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public int EstadoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        // Propiedades de navegación
        public virtual Categoria? Categoria { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Estado Estado { get; set; } = null!;
    }
} 