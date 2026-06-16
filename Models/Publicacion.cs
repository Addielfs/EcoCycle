using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Models
{
    public class Publicacion
    {
        [Key]
        public int IdPublicacion { get; set; }

        public int IdUsuario { get; set; }

        public int IdMaterial { get; set; }

        public int IdCentro { get; set; }

        public string Descripcion { get; set; }

        public decimal Cantidad { get; set; }

        public string Ubicacion { get; set; }

        public string Imagen { get; set; }

        public DateTime FechaPublicacion { get; set; }
    }
}