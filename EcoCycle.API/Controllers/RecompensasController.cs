using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecompensasController : ControllerBase
    {
        private readonly IRecompensaService _recompensaService;

        public RecompensasController(IRecompensaService recompensaService)
        {
            _recompensaService = recompensaService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var recompensas = await _recompensaService.GetAllAsync();
            return Ok(recompensas);
        }

        [HttpGet("usuario/{idUsuario}")]
        [Authorize]
        public async Task<IActionResult> GetPuntosByUsuario(int idUsuario)
        {
            var puntos = await _recompensaService.GetPuntosByUsuarioAsync(idUsuario);
            return Ok(puntos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRecompensaDto dto)
        {
            var recompensa = await _recompensaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPuntosByUsuario), new { idUsuario = recompensa.IdUsuario }, recompensa);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRecompensaDto dto)
        {
            if (id != dto.IdRecompensa)
                return BadRequest(new { error = "El ID no coincide" });

            try
            {
                await _recompensaService.UpdateAsync(dto);
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
                await _recompensaService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
