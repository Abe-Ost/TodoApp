using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de las entidades
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique();

            modelBuilder.Entity<Estado>()
                .HasIndex(e => e.NombreEstado)
                .IsUnique();

            // Configuración de las relaciones
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tareas)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tareas)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Estado)
                .WithMany(e => e.Tareas)
                .HasForeignKey(t => t.EstadoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Datos semilla para Estados
            modelBuilder.Entity<Estado>().HasData(
                new Estado { ID = 1, NombreEstado = "Pendiente" },
                new Estado { ID = 2, NombreEstado = "En Progreso" },
                new Estado { ID = 3, NombreEstado = "Completada" }
            );
        }
    }
} 