using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Repositories
{
    public class PublicacionRepository : BaseRepository<Publicacion>, IPublicacionRepository
    {
        public PublicacionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Publicacion>> GetByMaterialAsync(int idMaterial)
        {
            return await _context.Publicaciones
                .Where(p => p.IdMaterial == idMaterial)
                .Include(p => p.Usuario)
                .Include(p => p.Material)
                .ToListAsync();
        }

        public async Task<IEnumerable<Publicacion>> GetByUsuarioAsync(int idUsuario)
        {
            return await _context.Publicaciones
                .Where(p => p.IdUsuario == idUsuario)
                .Include(p => p.Material)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Publicacion>> GetAllAsync()
        {
            return await _context.Publicaciones
                .Include(p => p.Usuario)
                .Include(p => p.Material)
                .ToListAsync();
        }

        public override async Task<Publicacion?> GetByIdAsync(int id)
        {
            return await _context.Publicaciones
                .Include(p => p.Usuario)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(p => p.IdPublicacion == id);
        }
    }
}
