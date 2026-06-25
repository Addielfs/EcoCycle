using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Repositories
{
    public class RecompensaRepository : BaseRepository<Recompensa>, IRecompensaRepository
    {
        public RecompensaRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<Recompensa>> GetAllAsync()
        {
            return await _context.Recompensas
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.FechaRegistro)
                .ToListAsync();
        }

        public async Task<int> GetPuntosByUsuarioAsync(int idUsuario)
        {
            return await _context.Recompensas
                .Where(r => r.IdUsuario == idUsuario)
                .SumAsync(r => r.Puntos);
        }

        public async Task<IEnumerable<Recompensa>> GetByUsuarioAsync(int idUsuario)
        {
            return await _context.Recompensas
                .Include(r => r.Usuario)
                .Where(r => r.IdUsuario == idUsuario)
                .OrderByDescending(r => r.FechaRegistro)
                .ToListAsync();
        }

        public async Task<Recompensa?> GetByPublicacionIdAsync(int idPublicacion)
        {
            return await _context.Recompensas
                .FirstOrDefaultAsync(r => r.IdPublicacion == idPublicacion);
        }
    }
}
