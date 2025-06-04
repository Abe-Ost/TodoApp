using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.DTOs;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly TodoDbContext _context;
        private readonly TokenService _tokenService;

        public AccountController(TodoDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<string>> Registro(RegistroDTO registroDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == registroDto.NombreUsuario))
                return BadRequest("El nombre de usuario ya está en uso");

            if (await _context.Usuarios.AnyAsync(u => u.Correo == registroDto.Correo))
                return BadRequest("El correo electrónico ya está registrado");

            // Solo permitir la creación de administradores si la solicitud viene de un administrador
            if (registroDto.Rol == UserRole.Admin)
            {
                if (!User.Identity?.IsAuthenticated ?? true)
                    return Unauthorized("No tienes permisos para crear usuarios administradores");

                if (!User.IsInRole(UserRole.Admin.ToString()))
                    return Unauthorized("Solo los administradores pueden crear otros administradores");
            }

            var usuario = new Usuario
            {
                NombreUsuario = registroDto.NombreUsuario,
                Correo = registroDto.Correo,
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(registroDto.Contrasena),
                FechaCreacion = DateTime.UtcNow,
                Rol = registroDto.Rol ?? UserRole.User
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(_tokenService.GenerateToken(usuario));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == loginDto.NombreUsuario);

            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Contrasena, usuario.ContrasenaHash))
                return Unauthorized("Usuario o contraseña incorrectos");

            return Ok(_tokenService.GenerateToken(usuario));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Tareas)
                .Select(u => new UsuarioResponseDTO
                {
                    ID = u.ID,
                    NombreUsuario = u.NombreUsuario,
                    Correo = u.Correo,
                    FechaCreacion = u.FechaCreacion,
                    Rol = u.Rol,
                    TotalTareas = u.Tareas.Count,
                    TareasPendientes = u.Tareas.Count(t => t.EstadoId == 1),
                    TareasEnProgreso = u.Tareas.Count(t => t.EstadoId == 2),
                    TareasCompletadas = u.Tareas.Count(t => t.EstadoId == 3)
                })
                .ToListAsync();

            return usuarios;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("usuarios/{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Tareas)
                .FirstOrDefaultAsync(u => u.ID == id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            return new UsuarioResponseDTO
            {
                ID = usuario.ID,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                FechaCreacion = usuario.FechaCreacion,
                Rol = usuario.Rol,
                TotalTareas = usuario.Tareas.Count,
                TareasPendientes = usuario.Tareas.Count(t => t.EstadoId == 1),
                TareasEnProgreso = usuario.Tareas.Count(t => t.EstadoId == 2),
                TareasCompletadas = usuario.Tareas.Count(t => t.EstadoId == 3)
            };
        }
    }
} 