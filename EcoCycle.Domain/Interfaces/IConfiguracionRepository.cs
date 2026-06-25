using EcoCycle.Domain.Entities;

namespace EcoCycle.Domain.Interfaces
{
    public interface IConfiguracionRepository : IBaseRepository<Configuracion>
    {
        Task<Configuracion?> GetByClaveAsync(string clave);
    }
}
