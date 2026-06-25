using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoCycle.Domain.Entities
{
    [Table("Recompensas")]
    public class Recompensa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRecompensa { get; set; }

        public int IdUsuario { get; set; }

        public int Puntos { get; set; }

        public DateOnly FechaRegistro { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public int? IdPublicacion { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey("IdPublicacion")]
        public Publicacion? Publicacion { get; set; }
    }
}
