using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.DTOs;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TareasController(TodoDbContext context)
        {
            _context = context;
        }

        private int GetUsuarioId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        // GET: api/Tareas/usuario/{userId}
        [Authorize(Roles = "Admin")]
        [HttpGet("usuario/{userId}")]
        public async Task<ActionResult<IEnumerable<TareaResponseDTO>>> GetTareasPorUsuario(int userId)
        {
            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var tareas = await _context.Tareas
                .Include(t => t.Categoria)
                .Include(t => t.Usuario)
                .Include(t => t.Estado)
                .Where(t => t.UsuarioId == userId)
                .Select(t => new TareaResponseDTO
                {
                    ID = t.ID,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    CategoriaId = t.CategoriaId,
                    CategoriaNombre = t.Categoria != null ? t.Categoria.Nombre : null,
                    UsuarioId = t.UsuarioId,
                    NombreUsuario = t.Usuario.NombreUsuario,
                    EstadoId = t.EstadoId,
                    NombreEstado = t.Estado.NombreEstado,
                    FechaCreacion = t.FechaCreacion,
                    FechaVencimiento = t.FechaVencimiento
                })
                .ToListAsync();

            return tareas;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaResponseDTO>>> GetTareas()
        {
            var usuarioId = GetUsuarioId();
            var esAdmin = User.IsInRole(UserRole.Admin.ToString());

            var query = _context.Tareas
                .Include(t => t.Categoria)
                .Include(t => t.Usuario)
                .Include(t => t.Estado)
                .AsQueryable();

            // Si no es admin, solo ver sus propias tareas
            if (!esAdmin)
            {
                query = query.Where(t => t.UsuarioId == usuarioId);
            }

            var tareas = await query
                .Select(t => new TareaResponseDTO
                {
                    ID = t.ID,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    CategoriaId = t.CategoriaId,
                    CategoriaNombre = t.Categoria != null ? t.Categoria.Nombre : null,
                    UsuarioId = t.UsuarioId,
                    NombreUsuario = t.Usuario.NombreUsuario,
                    EstadoId = t.EstadoId,
                    NombreEstado = t.Estado.NombreEstado,
                    FechaCreacion = t.FechaCreacion,
                    FechaVencimiento = t.FechaVencimiento
                })
                .ToListAsync();

            return tareas;
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaResponseDTO>> GetTarea(int id)
        {
            var usuarioId = GetUsuarioId();
            var esAdmin = User.IsInRole(UserRole.Admin.ToString());

            var tarea = await _context.Tareas
                .Include(t => t.Categoria)
                .Include(t => t.Usuario)
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(t => t.ID == id);

            if (tarea == null)
            {
                return NotFound();
            }

            // Verificar si el usuario tiene acceso a esta tarea
            if (!esAdmin && tarea.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            return new TareaResponseDTO
            {
                ID = tarea.ID,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                CategoriaId = tarea.CategoriaId,
                CategoriaNombre = tarea.Categoria?.Nombre,
                UsuarioId = tarea.UsuarioId,
                NombreUsuario = tarea.Usuario.NombreUsuario,
                EstadoId = tarea.EstadoId,
                NombreEstado = tarea.Estado.NombreEstado,
                FechaCreacion = tarea.FechaCreacion,
                FechaVencimiento = tarea.FechaVencimiento
            };
        }

        // POST: api/Tareas
        [HttpPost]
        public async Task<ActionResult<TareaResponseDTO>> CreateTarea(CrearTareaDTO crearTareaDto)
        {
            var usuarioId = GetUsuarioId();

            // Validar que la categoría existe si se proporciona
            if (crearTareaDto.CategoriaId.HasValue)
            {
                var categoriaExiste = await _context.Categorias
                    .AnyAsync(c => c.ID == crearTareaDto.CategoriaId);
                if (!categoriaExiste)
                {
                    return BadRequest("La categoría especificada no existe");
                }
            }

            // Validar que el estado existe
            var estadoExiste = await _context.Estados
                .AnyAsync(e => e.ID == crearTareaDto.EstadoId);
            if (!estadoExiste)
            {
                return BadRequest("El estado especificado no existe");
            }

            var tarea = new Tarea
            {
                Titulo = crearTareaDto.Titulo,
                Descripcion = crearTareaDto.Descripcion,
                CategoriaId = crearTareaDto.CategoriaId,
                UsuarioId = usuarioId,
                EstadoId = crearTareaDto.EstadoId,
                FechaCreacion = DateTime.UtcNow,
                FechaVencimiento = crearTareaDto.FechaVencimiento
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            // Cargar las relaciones para el DTO de respuesta
            await _context.Entry(tarea)
                .Reference(t => t.Categoria)
                .LoadAsync();
            await _context.Entry(tarea)
                .Reference(t => t.Usuario)
                .LoadAsync();
            await _context.Entry(tarea)
                .Reference(t => t.Estado)
                .LoadAsync();

            return CreatedAtAction(
                nameof(GetTarea),
                new { id = tarea.ID },
                new TareaResponseDTO
                {
                    ID = tarea.ID,
                    Titulo = tarea.Titulo,
                    Descripcion = tarea.Descripcion,
                    CategoriaId = tarea.CategoriaId,
                    CategoriaNombre = tarea.Categoria?.Nombre,
                    UsuarioId = tarea.UsuarioId,
                    NombreUsuario = tarea.Usuario.NombreUsuario,
                    EstadoId = tarea.EstadoId,
                    NombreEstado = tarea.Estado.NombreEstado,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaVencimiento = tarea.FechaVencimiento
                });
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea(int id, ActualizarTareaDTO actualizarTareaDto)
        {
            var usuarioId = GetUsuarioId();
            var esAdmin = User.IsInRole(UserRole.Admin.ToString());

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            // Verificar si el usuario tiene permiso para actualizar esta tarea
            if (!esAdmin && tarea.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            // Validar que la categoría existe si se proporciona
            if (actualizarTareaDto.CategoriaId.HasValue)
            {
                var categoriaExiste = await _context.Categorias
                    .AnyAsync(c => c.ID == actualizarTareaDto.CategoriaId);
                if (!categoriaExiste)
                {
                    return BadRequest("La categoría especificada no existe");
                }
            }

            // Validar que el estado existe
            var estadoExiste = await _context.Estados
                .AnyAsync(e => e.ID == actualizarTareaDto.EstadoId);
            if (!estadoExiste)
            {
                return BadRequest("El estado especificado no existe");
            }

            tarea.Titulo = actualizarTareaDto.Titulo;
            tarea.Descripcion = actualizarTareaDto.Descripcion;
            tarea.CategoriaId = actualizarTareaDto.CategoriaId;
            tarea.EstadoId = actualizarTareaDto.EstadoId;
            tarea.FechaVencimiento = actualizarTareaDto.FechaVencimiento;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var usuarioId = GetUsuarioId();
            var esAdmin = User.IsInRole(UserRole.Admin.ToString());

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            // Verificar si el usuario tiene permiso para eliminar esta tarea
            if (!esAdmin && tarea.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.ID == id);
        }
    }
} 