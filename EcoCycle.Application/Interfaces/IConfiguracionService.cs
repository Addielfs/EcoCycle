using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface IConfiguracionService
    {
        Task<ConfiguracionDto?> GetByClaveAsync(string clave);
        Task<IEnumerable<ConfiguracionDto>> GetAllAsync();
        Task UpdateAsync(UpdateConfiguracionDto dto);
    }
}
