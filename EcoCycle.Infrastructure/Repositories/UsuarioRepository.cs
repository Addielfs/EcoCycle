using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<IEnumerable<Publicacion>> GetPublicacionesByUsuarioAsync(int idUsuario)
        {
            return await _context.Publicaciones
                .Where(p => p.IdUsuario == idUsuario)
                .Include(p => p.Material)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recompensa>> GetRecompensasByUsuarioAsync(int idUsuario)
        {
            return await _context.Recompensas
                .Where(r => r.IdUsuario == idUsuario)
                .ToListAsync();
        }
    }
}
