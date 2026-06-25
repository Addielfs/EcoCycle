using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;

namespace EcoCycle.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;

        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MaterialDto>> GetAllAsync()
        {
            var materiales = await _repository.GetAllAsync();
            return materiales.Select(MapToDto);
        }

        public async Task<MaterialDto?> GetByIdAsync(int id)
        {
            var material = await _repository.GetByIdAsync(id);
            return material == null ? null : MapToDto(material);
        }

        public async Task<MaterialDto> CreateAsync(CreateMaterialDto dto)
        {
            var existing = await _repository.GetByNombreAsync(dto.NombreMaterial);
            if (existing != null)
                throw new InvalidOperationException("El material ya existe");

            var material = new Material
            {
                NombreMaterial = dto.NombreMaterial,
                Descripcion = dto.Descripcion
            };

            var created = await _repository.AddAsync(material);
            return MapToDto(created);
        }

        public async Task UpdateAsync(UpdateMaterialDto dto)
        {
            var material = await _repository.GetByIdAsync(dto.IdMaterial);
            if (material == null)
                throw new KeyNotFoundException("Material no encontrado");

            material.NombreMaterial = dto.NombreMaterial;
            material.Descripcion = dto.Descripcion;

            await _repository.UpdateAsync(material);
        }

        public async Task DeleteAsync(int id)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null)
                throw new KeyNotFoundException("Material no encontrado");

            await _repository.DeleteAsync(material);
        }

        private static MaterialDto MapToDto(Material material) => new()
        {
            IdMaterial = material.IdMaterial,
            NombreMaterial = material.NombreMaterial,
            Descripcion = material.Descripcion
        };
    }
}
