using EcoCycle.Domain.Entities;

namespace EcoCycle.Domain.Interfaces
{
    public interface IPublicacionRepository : IBaseRepository<Publicacion>
    {
        Task<IEnumerable<Publicacion>> GetByMaterialAsync(int idMaterial);
        Task<IEnumerable<Publicacion>> GetByUsuarioAsync(int idUsuario);
    }
}
