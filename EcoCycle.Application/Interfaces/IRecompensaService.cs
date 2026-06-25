using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface IRecompensaService
    {
        Task<PuntosUsuarioDto> GetPuntosByUsuarioAsync(int idUsuario);
        Task<IEnumerable<RecompensaDto>> GetAllAsync();
        Task<RecompensaDto> CreateAsync(CreateRecompensaDto dto);
        Task UpdateAsync(UpdateRecompensaDto dto);
        Task DeleteAsync(int id);
    }
}
