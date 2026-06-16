using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}