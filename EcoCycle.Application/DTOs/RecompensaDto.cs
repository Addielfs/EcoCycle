using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Application.DTOs
{
    public class CreateRecompensaDto
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Puntos { get; set; }
    }

    public class RecompensaDto
    {
        public int IdRecompensa { get; set; }
        public int IdUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
        public int Puntos { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public int? IdPublicacion { get; set; }
    }

    public class UpdateRecompensaDto
    {
        [Required]
        public int IdRecompensa { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Puntos { get; set; }
    }

    public class PuntosUsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
        public int TotalPuntos { get; set; }
        public List<RecompensaDto> Historial { get; set; } = new();
    }
}
