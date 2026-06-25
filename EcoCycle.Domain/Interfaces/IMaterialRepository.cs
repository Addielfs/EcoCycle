using EcoCycle.Domain.Entities;

namespace EcoCycle.Domain.Interfaces
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        Task<Material?> GetByNombreAsync(string nombre);
    }
}
