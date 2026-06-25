using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoCycle.Domain.Entities
{
    [Table("CentrosReciclaje")]
    public class CentroReciclaje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCentro { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreCentro { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Direccion { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Latitud { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Longitud { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        public int Capacidad { get; set; }
    }
}
