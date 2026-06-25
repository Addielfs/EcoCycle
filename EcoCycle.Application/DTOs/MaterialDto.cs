using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Application.DTOs
{
    public class CreateMaterialDto
    {
        [Required(ErrorMessage = "El nombre del material es obligatorio")]
        [MaxLength(100)]
        public string NombreMaterial { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }

    public class UpdateMaterialDto
    {
        [Required]
        public int IdMaterial { get; set; }

        [Required(ErrorMessage = "El nombre del material es obligatorio")]
        [MaxLength(100)]
        public string NombreMaterial { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }

    public class MaterialDto
    {
        public int IdMaterial { get; set; }
        public string NombreMaterial { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
