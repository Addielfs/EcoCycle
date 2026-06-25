using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Application.DTOs
{
    public class CreatePublicacionDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El material es obligatorio")]
        public int IdMaterial { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0.01, 999999.99, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public decimal Cantidad { get; set; }

        [MaxLength(255)]
        public string? Ubicacion { get; set; }

        [Range(-90, 90)]
        public decimal? Latitud { get; set; }

        [Range(-180, 180)]
        public decimal? Longitud { get; set; }

        [MaxLength(255)]
        public string? Imagen { get; set; }
    }

    public class UpdatePublicacionDto
    {
        [Required]
        public int IdPublicacion { get; set; }

        [Required]
        public int IdMaterial { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Cantidad { get; set; }

        [MaxLength(255)]
        public string? Ubicacion { get; set; }

        [Range(-90, 90)]
        public decimal? Latitud { get; set; }

        [Range(-180, 180)]
        public decimal? Longitud { get; set; }

        [MaxLength(255)]
        public string? Imagen { get; set; }
    }

    public class PublicacionDto
    {
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
        public int IdMaterial { get; set; }
        public string? NombreMaterial { get; set; }
        public string? Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public string? Ubicacion { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? Imagen { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
