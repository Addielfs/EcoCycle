using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;

namespace EcoCycle.Application.Services
{
    public class PublicacionService : IPublicacionService
    {
        private readonly IPublicacionRepository _repository;
        private readonly IRecompensaRepository _recompensaRepository;
        private readonly IConfiguracionRepository _configuracionRepository;

        public PublicacionService(
            IPublicacionRepository repository,
            IRecompensaRepository recompensaRepository,
            IConfiguracionRepository configuracionRepository)
        {
            _repository = repository;
            _recompensaRepository = recompensaRepository;
            _configuracionRepository = configuracionRepository;
        }

        public async Task<IEnumerable<PublicacionDto>> GetAllAsync()
        {
            var publicaciones = await _repository.GetAllAsync();
            return publicaciones.Select(MapToDto);
        }

        public async Task<PublicacionDto?> GetByIdAsync(int id)
        {
            var publicacion = await _repository.GetByIdAsync(id);
            return publicacion == null ? null : MapToDto(publicacion);
        }

        public async Task<IEnumerable<PublicacionDto>> GetByMaterialAsync(int idMaterial)
        {
            var publicaciones = await _repository.GetByMaterialAsync(idMaterial);
            return publicaciones.Select(MapToDto);
        }

        public async Task<IEnumerable<PublicacionDto>> GetByUsuarioAsync(int idUsuario)
        {
            var publicaciones = await _repository.GetByUsuarioAsync(idUsuario);
            return publicaciones.Select(MapToDto);
        }

        public async Task<PublicacionDto> CreateAsync(CreatePublicacionDto dto)
        {
            var publicacion = new Publicacion
            {
                IdUsuario = dto.IdUsuario,
                IdMaterial = dto.IdMaterial,
                Descripcion = dto.Descripcion,
                Cantidad = dto.Cantidad,
                Ubicacion = dto.Ubicacion,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                Imagen = dto.Imagen,
                FechaPublicacion = DateTime.UtcNow
            };

            var created = await _repository.AddAsync(publicacion);

            var config = await _configuracionRepository.GetByClaveAsync("ConversionFactor");
            var factor = config != null && int.TryParse(config.Valor, out var f) ? f : 10;
            var puntos = (int)(created.Cantidad * factor);

            var recompensa = new Recompensa
            {
                IdUsuario = created.IdUsuario,
                Puntos = puntos,
                IdPublicacion = created.IdPublicacion,
                FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await _recompensaRepository.AddAsync(recompensa);

            return await GetByIdAsync(created.IdPublicacion) ?? MapToDto(created);
        }

        public async Task UpdateAsync(UpdatePublicacionDto dto)
        {
            var publicacion = await _repository.GetByIdAsync(dto.IdPublicacion);
            if (publicacion == null)
                throw new KeyNotFoundException("Publicación no encontrada");

            publicacion.IdMaterial = dto.IdMaterial;
            publicacion.Descripcion = dto.Descripcion;
            publicacion.Cantidad = dto.Cantidad;
            publicacion.Ubicacion = dto.Ubicacion;
            publicacion.Latitud = dto.Latitud;
            publicacion.Longitud = dto.Longitud;
            publicacion.Imagen = dto.Imagen;

            await _repository.UpdateAsync(publicacion);

            var recompensa = await _recompensaRepository.GetByPublicacionIdAsync(publicacion.IdPublicacion);
            if (recompensa != null)
            {
                var config = await _configuracionRepository.GetByClaveAsync("ConversionFactor");
                var factor = config != null && int.TryParse(config.Valor, out var f) ? f : 10;
                recompensa.Puntos = (int)(publicacion.Cantidad * factor);
                await _recompensaRepository.UpdateAsync(recompensa);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var publicacion = await _repository.GetByIdAsync(id);
            if (publicacion == null)
                throw new KeyNotFoundException("Publicación no encontrada");

            await _repository.DeleteAsync(publicacion);
        }

        public async Task UpdateImagenAsync(int id, string imagenUrl)
        {
            var publicacion = await _repository.GetByIdAsync(id);
            if (publicacion == null)
                throw new KeyNotFoundException("Publicación no encontrada");

            publicacion.Imagen = imagenUrl;
            await _repository.UpdateAsync(publicacion);
        }

        public async Task ClearImagenAsync(int id)
        {
            var publicacion = await _repository.GetByIdAsync(id);
            if (publicacion == null)
                throw new KeyNotFoundException("Publicación no encontrada");

            publicacion.Imagen = null;
            await _repository.UpdateAsync(publicacion);
        }

        private static PublicacionDto MapToDto(Publicacion p) => new()
        {
            IdPublicacion = p.IdPublicacion,
            IdUsuario = p.IdUsuario,
            CorreoUsuario = p.Usuario?.Correo,
            IdMaterial = p.IdMaterial,
            NombreMaterial = p.Material?.NombreMaterial,
            Descripcion = p.Descripcion,
            Cantidad = p.Cantidad,
            Ubicacion = p.Ubicacion,
            Latitud = p.Latitud,
            Longitud = p.Longitud,
            Imagen = p.Imagen,
            FechaPublicacion = p.FechaPublicacion
        };
    }
}
