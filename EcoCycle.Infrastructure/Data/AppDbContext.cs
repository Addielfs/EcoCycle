using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Material> Materiales => Set<Material>();
        public DbSet<CentroReciclaje> CentrosReciclaje => Set<CentroReciclaje>();
        public DbSet<Publicacion> Publicaciones => Set<Publicacion>();
        public DbSet<Recompensa> Recompensas => Set<Recompensa>();
        public DbSet<Configuracion> Configuraciones => Set<Configuracion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
