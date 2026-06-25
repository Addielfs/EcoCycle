using System.Security.Claims;
using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoCycle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicacionesController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;
        private readonly IImageStorageService _imageStorageService;

        public PublicacionesController(
            IPublicacionService publicacionService,
            IImageStorageService imageStorageService)
        {
            _publicacionService = publicacionService;
            _imageStorageService = imageStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publicaciones = await _publicacionService.GetAllAsync();
            return Ok(publicaciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publicacion = await _publicacionService.GetByIdAsync(id);
            if (publicacion == null)
                return NotFound(new { error = "Publicación no encontrada" });

            return Ok(publicacion);
        }

        [HttpGet("material/{idMaterial}")]
        public async Task<IActionResult> GetByMaterial(int idMaterial)
        {
            var publicaciones = await _publicacionService.GetByMaterialAsync(idMaterial);
            return Ok(publicaciones);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUsuario(int idUsuario)
        {
            var publicaciones = await _publicacionService.GetByUsuarioAsync(idUsuario);
            return Ok(publicaciones);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePublicacionDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            dto.IdUsuario = userId;

            var publicacion = await _publicacionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = publicacion.IdPublicacion }, publicacion);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePublicacionDto dto)
        {
            if (id != dto.IdPublicacion)
                return BadRequest(new { error = "El ID no coincide" });

            try
            {
                var publicacion = await _publicacionService.GetByIdAsync(id);
                if (publicacion == null)
                    return NotFound(new { error = "Publicación no encontrada" });

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var isAdmin = User.IsInRole("Admin");

                if (publicacion.IdUsuario != userId && !isAdmin)
                    return Forbid();

                await _publicacionService.UpdateAsync(dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var publicacion = await _publicacionService.GetByIdAsync(id);
                if (publicacion == null)
                    return NotFound(new { error = "Publicación no encontrada" });

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var isAdmin = User.IsInRole("Admin");

                if (publicacion.IdUsuario != userId && !isAdmin)
                    return Forbid();

                if (!string.IsNullOrEmpty(publicacion.Imagen))
                    await _imageStorageService.DeleteImageAsync(publicacion.Imagen);

                await _publicacionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/imagen")]
        [Authorize]
        [RequestSizeLimit(10 * 1024 * 1024)]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "No se proporcionó un archivo válido" });

            try
            {
                var publicacion = await _publicacionService.GetByIdAsync(id);
                if (publicacion == null)
                    return NotFound(new { error = "Publicación no encontrada" });

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var isAdmin = User.IsInRole("Admin");

                if (publicacion.IdUsuario != userId && !isAdmin)
                    return Forbid();

                if (!string.IsNullOrEmpty(publicacion.Imagen))
                    await _imageStorageService.DeleteImageAsync(publicacion.Imagen);

                await using var stream = file.OpenReadStream();
                var imageUrl = await _imageStorageService.SaveImageAsync(stream, file.FileName);
                await _publicacionService.UpdateImagenAsync(id, imageUrl);

                return Ok(new { imagen = imageUrl });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}/imagen")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var publicacion = await _publicacionService.GetByIdAsync(id);
            if (publicacion == null)
                return NotFound(new { error = "Publicación no encontrada" });

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isAdmin = User.IsInRole("Admin");

            if (publicacion.IdUsuario != userId && !isAdmin)
                return Forbid();

            if (!string.IsNullOrEmpty(publicacion.Imagen))
            {
                await _imageStorageService.DeleteImageAsync(publicacion.Imagen);
                await _publicacionService.ClearImagenAsync(id);
            }

            return NoContent();
        }
    }
}
