using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.DTOs
{
    public class CrearTareaDTO
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(255, ErrorMessage = "El título no puede exceder los 255 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public int EstadoId { get; set; }

        public DateTime? FechaVencimiento { get; set; }
    }

    public class ActualizarTareaDTO
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(255, ErrorMessage = "El título no puede exceder los 255 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public int EstadoId { get; set; }

        public DateTime? FechaVencimiento { get; set; }
    }

    public class TareaResponseDTO
    {
        public int ID { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int? CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int EstadoId { get; set; }
        public string NombreEstado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
} 