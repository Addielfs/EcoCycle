using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CentrosReciclajeController : ControllerBase
    {
        private readonly ICentroReciclajeService _centroService;

        public CentrosReciclajeController(ICentroReciclajeService centroService)
        {
            _centroService = centroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var centros = await _centroService.GetAllAsync();
            return Ok(centros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var centro = await _centroService.GetByIdAsync(id);
            if (centro == null)
                return NotFound(new { error = "Centro no encontrado" });

            return Ok(centro);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCentroReciclajeDto dto)
        {
            var centro = await _centroService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = centro.IdCentro }, centro);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCentroReciclajeDto dto)
        {
            if (id != dto.IdCentro)
                return BadRequest(new { error = "El ID no coincide" });

            try
            {
                await _centroService.UpdateAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _centroService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
