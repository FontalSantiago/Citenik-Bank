using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Plan> Planes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Prestamo> Prestamos { get; set; } 

        public DbSet<Cuota> Cuotas { get; set; }

        public DbSet<Composicion> Composiciones { get; set; }

        public DbSet<Concepto> Conceptos { get; set; }
    }
}
