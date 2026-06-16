using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Models
{
    public class Material
    {
        [Key]
        public int IdMaterial { get; set; }

        public string NombreMaterial { get; set; }

        public string Descripcion { get; set; }
    }
}