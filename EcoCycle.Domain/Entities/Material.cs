using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoCycle.Domain.Entities
{
    [Table("Materiales")]
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMaterial { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreMaterial { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}
