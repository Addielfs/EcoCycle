using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface ICentroReciclajeService
    {
        Task<IEnumerable<CentroReciclajeDto>> GetAllAsync();
        Task<CentroReciclajeDto?> GetByIdAsync(int id);
        Task<CentroReciclajeDto> CreateAsync(CreateCentroReciclajeDto dto);
        Task UpdateAsync(UpdateCentroReciclajeDto dto);
        Task DeleteAsync(int id);
    }
}
