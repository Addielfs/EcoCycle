using System.ComponentModel.DataAnnotations;

namespace EcoCycle.Application.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [MaxLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [MaxLength(255)]
        public string Contrasena { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(255)]
        public string? Direccion { get; set; }
    }

    public class LoginDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contrasena { get; set; } = string.Empty;
    }

    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expira { get; set; }
        public int IdUsuario { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
