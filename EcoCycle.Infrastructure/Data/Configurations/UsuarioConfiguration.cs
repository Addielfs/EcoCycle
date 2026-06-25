using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoCycle.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.Correo)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Correo)
                .IsUnique();

            builder.Property(u => u.Contrasena)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Telefono)
                .HasMaxLength(20);

            builder.Property(u => u.Direccion)
                .HasMaxLength(255);

            builder.Property(u => u.Rol)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Usuario");

            builder.Property(u => u.FechaRegistro)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(u => u.Publicaciones)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Recompensas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
