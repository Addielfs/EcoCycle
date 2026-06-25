using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDto>> GetAllAsync();
        Task<MaterialDto?> GetByIdAsync(int id);
        Task<MaterialDto> CreateAsync(CreateMaterialDto dto);
        Task UpdateAsync(UpdateMaterialDto dto);
        Task DeleteAsync(int id);
    }
}
