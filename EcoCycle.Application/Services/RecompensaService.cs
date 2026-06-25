using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;

namespace EcoCycle.Application.Services
{
    public class RecompensaService : IRecompensaService
    {
        private readonly IRecompensaRepository _repository;

        public RecompensaService(IRecompensaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PuntosUsuarioDto> GetPuntosByUsuarioAsync(int idUsuario)
        {
            var totalPuntos = await _repository.GetPuntosByUsuarioAsync(idUsuario);
            var historial = await _repository.GetByUsuarioAsync(idUsuario);

            var correo = historial.FirstOrDefault()?.Usuario?.Correo;

            return new PuntosUsuarioDto
            {
                IdUsuario = idUsuario,
                CorreoUsuario = correo,
                TotalPuntos = totalPuntos,
                Historial = historial.Select(r => new RecompensaDto
                {
                    IdRecompensa = r.IdRecompensa,
                    IdUsuario = r.IdUsuario,
                    CorreoUsuario = r.Usuario?.Correo,
                    Puntos = r.Puntos,
                    FechaRegistro = r.FechaRegistro,
                    IdPublicacion = r.IdPublicacion
                }).ToList()
            };
        }

        public async Task<IEnumerable<RecompensaDto>> GetAllAsync()
        {
            var recompensas = await _repository.GetAllAsync();
            return recompensas.Select(r => new RecompensaDto
            {
                IdRecompensa = r.IdRecompensa,
                IdUsuario = r.IdUsuario,
                CorreoUsuario = r.Usuario?.Correo,
                Puntos = r.Puntos,
                FechaRegistro = r.FechaRegistro,
                IdPublicacion = r.IdPublicacion
            });
        }

        public async Task<RecompensaDto> CreateAsync(CreateRecompensaDto dto)
        {
            var recompensa = new Recompensa
            {
                IdUsuario = dto.IdUsuario,
                Puntos = dto.Puntos,
                FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            var created = await _repository.AddAsync(recompensa);

            return new RecompensaDto
            {
                IdRecompensa = created.IdRecompensa,
                IdUsuario = created.IdUsuario,
                Puntos = created.Puntos,
                FechaRegistro = created.FechaRegistro,
                IdPublicacion = created.IdPublicacion
            };
        }

        public async Task UpdateAsync(UpdateRecompensaDto dto)
        {
            var recompensa = await _repository.GetByIdAsync(dto.IdRecompensa);
            if (recompensa == null)
                throw new KeyNotFoundException("Recompensa no encontrada");

            recompensa.Puntos = dto.Puntos;
            await _repository.UpdateAsync(recompensa);
        }

        public async Task DeleteAsync(int id)
        {
            var recompensa = await _repository.GetByIdAsync(id);
            if (recompensa == null)
                throw new KeyNotFoundException("Recompensa no encontrada");

            await _repository.DeleteAsync(recompensa);
        }
    }
}
