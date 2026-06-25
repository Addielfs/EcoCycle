using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoCycle.Domain.Entities
{
    [Table("Configuraciones")]
    public class Configuracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConfiguracion { get; set; }

        [Required]
        [MaxLength(100)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Valor { get; set; } = string.Empty;
    }
}
