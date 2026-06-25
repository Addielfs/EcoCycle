using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoCycle.Domain.Entities
{
    [Table("Publicaciones")]
    public class Publicacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPublicacion { get; set; }

        public int IdUsuario { get; set; }

        public int IdMaterial { get; set; }

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cantidad { get; set; }

        [MaxLength(255)]
        public string? Ubicacion { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Latitud { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? Longitud { get; set; }

        [MaxLength(255)]
        public string? Imagen { get; set; }

        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey("IdMaterial")]
        public Material Material { get; set; } = null!;
    }
}
