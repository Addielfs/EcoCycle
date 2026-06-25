using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.Materiales.AnyAsync())
            {
                var materiales = new List<Material>
                {
                    new() { NombreMaterial = "Papel", Descripcion = "Papel reciclable: peri\u00F3dicos, revistas, hojas" },
                    new() { NombreMaterial = "Cart\u00F3n", Descripcion = "Cart\u00F3n reciclable: cajas, empaques" },
                    new() { NombreMaterial = "Pl\u00E1stico", Descripcion = "Pl\u00E1stico reciclable: botellas, envases" },
                    new() { NombreMaterial = "Vidrio", Descripcion = "Vidrio reciclable: botellas, frascos" },
                    new() { NombreMaterial = "Metal", Descripcion = "Metal reciclable: latas, aluminio, hierro" },
                    new() { NombreMaterial = "Electr\u00F3nicos", Descripcion = "Residuos electr\u00F3nicos: cables, dispositivos" }
                };

                await context.Materiales.AddRangeAsync(materiales);
            }

            if (!await context.Usuarios.AnyAsync(u => u.Correo == "admin@ecocycle.com"))
            {
                var admin = new Usuario
                {
                    Correo = "admin@ecocycle.com",
                    Contrasena = BCryptHash("Admin123!"),
                    Rol = "Admin",
                    FechaRegistro = DateTime.UtcNow
                };

                context.Usuarios.Add(admin);
            }

            if (!await context.Configuraciones.AnyAsync(c => c.Clave == "ConversionFactor"))
            {
                context.Configuraciones.Add(new Configuracion
                {
                    Clave = "ConversionFactor",
                    Valor = "10"
                });
            }

            await context.SaveChangesAsync();
        }

        private static string BCryptHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
