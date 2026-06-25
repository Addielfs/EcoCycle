using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly IConfiguracionService _configuracionService;

        public ConfiguracionController(IConfiguracionService configuracionService)
        {
            _configuracionService = configuracionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var configs = await _configuracionService.GetAllAsync();
            return Ok(configs);
        }

        [HttpGet("{clave}")]
        public async Task<IActionResult> GetByClave(string clave)
        {
            var config = await _configuracionService.GetByClaveAsync(clave);
            if (config == null)
                return NotFound(new { error = "Configuración no encontrada" });

            return Ok(config);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateConfiguracionDto dto)
        {
            if (id != dto.IdConfiguracion)
                return BadRequest(new { error = "El ID no coincide" });

            try
            {
                await _configuracionService.UpdateAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
