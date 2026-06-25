using EcoCycle.Domain.Entities;

namespace EcoCycle.Domain.Interfaces
{
    public interface IRecompensaRepository : IBaseRepository<Recompensa>
    {
        Task<int> GetPuntosByUsuarioAsync(int idUsuario);
        Task<IEnumerable<Recompensa>> GetByUsuarioAsync(int idUsuario);
        Task<Recompensa?> GetByPublicacionIdAsync(int idPublicacion);
    }
}
