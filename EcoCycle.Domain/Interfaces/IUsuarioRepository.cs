using EcoCycle.Domain.Entities;

namespace EcoCycle.Domain.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> GetByCorreoAsync(string correo);
        Task<IEnumerable<Publicacion>> GetPublicacionesByUsuarioAsync(int idUsuario);
        Task<IEnumerable<Recompensa>> GetRecompensasByUsuarioAsync(int idUsuario);
    }
}
