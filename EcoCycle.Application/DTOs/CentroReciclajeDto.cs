using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Application.DTOs
{
    public class CreateCentroReciclajeDto
    {
        [Required(ErrorMessage = "El nombre del centro es obligatorio")]
        [MaxLength(150)]
        public string NombreCentro { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Direccion { get; set; }

        [Range(-90, 90)]
        public decimal? Latitud { get; set; }

        [Range(-180, 180)]
        public decimal? Longitud { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Capacidad { get; set; }
    }

    public class UpdateCentroReciclajeDto
    {
        [Required]
        public int IdCentro { get; set; }

        [Required(ErrorMessage = "El nombre del centro es obligatorio")]
        [MaxLength(150)]
        public string NombreCentro { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Direccion { get; set; }

        [Range(-90, 90)]
        public decimal? Latitud { get; set; }

        [Range(-180, 180)]
        public decimal? Longitud { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Capacidad { get; set; }
    }

    public class CentroReciclajeDto
    {
        public int IdCentro { get; set; }
        public string NombreCentro { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? Telefono { get; set; }
        public int Capacidad { get; set; }
    }
}
