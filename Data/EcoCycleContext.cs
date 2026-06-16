using Microsoft.EntityFrameworkCore;
using EcoCycle.Models;

namespace EcoCycle.Data
{
    public class EcoCycleContext : DbContext
    {
        public EcoCycleContext(DbContextOptions<EcoCycleContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<CentroReciclaje> CentrosReciclaje { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
    }
}