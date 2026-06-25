using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcoCycle.Application.DTOs;
using EcoCycle.Application.Interfaces;
using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EcoCycle.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<TokenDto> RegisterAsync(RegisterDto registerDto)
        {
            var existing = await _usuarioRepository.GetByCorreoAsync(registerDto.Correo);
            if (existing != null)
                throw new InvalidOperationException("El correo ya está registrado");

            var usuario = new Usuario
            {
                Correo = registerDto.Correo,
                Contrasena = BCryptHash(registerDto.Contrasena),
                Telefono = registerDto.Telefono,
                Direccion = registerDto.Direccion,
                FechaRegistro = DateTime.UtcNow
            };

            await _usuarioRepository.AddAsync(usuario);
            return GenerateToken(usuario);
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _usuarioRepository.GetByCorreoAsync(loginDto.Correo);
            if (usuario == null || !BCryptVerify(loginDto.Contrasena, usuario.Contrasena))
                throw new UnauthorizedAccessException("Credenciales inválidas");

            return GenerateToken(usuario);
        }

        private TokenDto GenerateToken(Usuario usuario)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expira = token.ValidTo,
                IdUsuario = usuario.IdUsuario,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
        }

        private static string BCryptHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool BCryptVerify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
