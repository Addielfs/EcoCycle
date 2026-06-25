using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Application.DTOs
{
    public class ConfiguracionDto
    {
        public int IdConfiguracion { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }

    public class UpdateConfiguracionDto
    {
        [Required]
        public int IdConfiguracion { get; set; }

        [Required]
        [MaxLength(500)]
        public string Valor { get; set; } = string.Empty;
    }
}
