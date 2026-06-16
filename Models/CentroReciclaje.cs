using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Models
{
    public class CentroReciclaje
    {
        [Key]
        public int IdCentro { get; set; }

        public string NombreCentro { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int Capacidad { get; set; }
    }
}