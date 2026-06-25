using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface IReporteService
    {
        Task<ReporteGeneralDto> GetGeneralAsync();
    }
}
