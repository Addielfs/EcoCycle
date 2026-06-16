using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Models
{
    public class Recompensa
    {
        [Key]
        public int IdRecompensa { get; set; }

        public int IdUsuario { get; set; }

        public int Puntos { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}