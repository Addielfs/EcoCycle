using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Interfaces;

namespace EcoCycle.Application.Services
{
    public class ConfiguracionService : IConfiguracionService
    {
        private readonly IConfiguracionRepository _repository;

        public ConfiguracionService(IConfiguracionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConfiguracionDto?> GetByClaveAsync(string clave)
        {
            var config = await _repository.GetByClaveAsync(clave);
            return config == null ? null : new ConfiguracionDto
            {
                IdConfiguracion = config.IdConfiguracion,
                Clave = config.Clave,
                Valor = config.Valor
            };
        }

        public async Task<IEnumerable<ConfiguracionDto>> GetAllAsync()
        {
            var configs = await _repository.GetAllAsync();
            return configs.Select(c => new ConfiguracionDto
            {
                IdConfiguracion = c.IdConfiguracion,
                Clave = c.Clave,
                Valor = c.Valor
            });
        }

        public async Task UpdateAsync(UpdateConfiguracionDto dto)
        {
            var config = await _repository.GetByIdAsync(dto.IdConfiguracion);
            if (config == null)
                throw new KeyNotFoundException("Configuración no encontrada");

            config.Valor = dto.Valor;
            await _repository.UpdateAsync(config);
        }
    }
}
