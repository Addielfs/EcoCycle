using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface IPublicacionService
    {
        Task<IEnumerable<PublicacionDto>> GetAllAsync();
        Task<PublicacionDto?> GetByIdAsync(int id);
        Task<IEnumerable<PublicacionDto>> GetByMaterialAsync(int idMaterial);
        Task<IEnumerable<PublicacionDto>> GetByUsuarioAsync(int idUsuario);
        Task<PublicacionDto> CreateAsync(CreatePublicacionDto dto);
        Task UpdateAsync(UpdatePublicacionDto dto);
        Task DeleteAsync(int id);
        Task UpdateImagenAsync(int id, string imagenUrl);
        Task ClearImagenAsync(int id);
    }
}
