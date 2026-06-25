using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;

namespace EcoCycle.Application.Services
{
    public class CentroReciclajeService : ICentroReciclajeService
    {
        private readonly ICentroReciclajeRepository _repository;

        public CentroReciclajeService(ICentroReciclajeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CentroReciclajeDto>> GetAllAsync()
        {
            var centros = await _repository.GetAllAsync();
            return centros.Select(MapToDto);
        }

        public async Task<CentroReciclajeDto?> GetByIdAsync(int id)
        {
            var centro = await _repository.GetByIdAsync(id);
            return centro == null ? null : MapToDto(centro);
        }

        public async Task<CentroReciclajeDto> CreateAsync(CreateCentroReciclajeDto dto)
        {
            var centro = new CentroReciclaje
            {
                NombreCentro = dto.NombreCentro,
                Direccion = dto.Direccion,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                Telefono = dto.Telefono,
                Capacidad = dto.Capacidad
            };

            var created = await _repository.AddAsync(centro);
            return MapToDto(created);
        }

        public async Task UpdateAsync(UpdateCentroReciclajeDto dto)
        {
            var centro = await _repository.GetByIdAsync(dto.IdCentro);
            if (centro == null)
                throw new KeyNotFoundException("Centro de reciclaje no encontrado");

            centro.NombreCentro = dto.NombreCentro;
            centro.Direccion = dto.Direccion;
            centro.Latitud = dto.Latitud;
            centro.Longitud = dto.Longitud;
            centro.Telefono = dto.Telefono;
            centro.Capacidad = dto.Capacidad;

            await _repository.UpdateAsync(centro);
        }

        public async Task DeleteAsync(int id)
        {
            var centro = await _repository.GetByIdAsync(id);
            if (centro == null)
                throw new KeyNotFoundException("Centro de reciclaje no encontrado");

            await _repository.DeleteAsync(centro);
        }

        private static CentroReciclajeDto MapToDto(CentroReciclaje c) => new()
        {
            IdCentro = c.IdCentro,
            NombreCentro = c.NombreCentro,
            Direccion = c.Direccion,
            Latitud = c.Latitud,
            Longitud = c.Longitud,
            Telefono = c.Telefono,
            Capacidad = c.Capacidad
        };
    }
}
