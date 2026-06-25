using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Interfaces;

namespace EcoCycle.Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IMaterialRepository _materialRepo;
        private readonly IPublicacionRepository _publicacionRepo;
        private readonly ICentroReciclajeRepository _centroRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IRecompensaRepository _recompensaRepo;

        public ReporteService(
            IMaterialRepository materialRepo,
            IPublicacionRepository publicacionRepo,
            ICentroReciclajeRepository centroRepo,
            IUsuarioRepository usuarioRepo,
            IRecompensaRepository recompensaRepo)
        {
            _materialRepo = materialRepo;
            _publicacionRepo = publicacionRepo;
            _centroRepo = centroRepo;
            _usuarioRepo = usuarioRepo;
            _recompensaRepo = recompensaRepo;
        }

        public async Task<ReporteGeneralDto> GetGeneralAsync()
        {
            var materiales = await _materialRepo.GetAllAsync();
            var publicaciones = await _publicacionRepo.GetAllAsync();
            var centros = await _centroRepo.GetAllAsync();
            var usuarios = await _usuarioRepo.GetAllAsync();
            var recompensas = await _recompensaRepo.GetAllAsync();

            return new ReporteGeneralDto
            {
                TotalMateriales = materiales.Count(),
                TotalPublicaciones = publicaciones.Count(),
                TotalCentros = centros.Count(),
                TotalUsuarios = usuarios.Count(),
                TotalPuntos = recompensas.Sum(r => r.Puntos),
                UltimasPublicaciones = publicaciones
                    .OrderByDescending(p => p.FechaPublicacion)
                    .Take(10)
                    .Select(p => new PublicacionDto
                    {
                        IdPublicacion = p.IdPublicacion,
                        IdUsuario = p.IdUsuario,
                        CorreoUsuario = p.Usuario?.Correo,
                        IdMaterial = p.IdMaterial,
                        NombreMaterial = p.Material?.NombreMaterial,
                        Descripcion = p.Descripcion,
                        Cantidad = p.Cantidad,
                        Ubicacion = p.Ubicacion,
                        FechaPublicacion = p.FechaPublicacion
                    }).ToList()
            };
        }
    }
}
