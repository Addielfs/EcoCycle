using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoCycle.Infrastructure.Data.Configurations
{
    public class PublicacionConfiguration : IEntityTypeConfiguration<Publicacion>
    {
        public void Configure(EntityTypeBuilder<Publicacion> builder)
        {
            builder.ToTable("Publicaciones");

            builder.HasKey(p => p.IdPublicacion);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(255);

            builder.Property(p => p.Cantidad)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Ubicacion)
                .HasMaxLength(255);

            builder.Property(p => p.Imagen)
                .HasMaxLength(255);

            builder.Property(p => p.Latitud)
                .HasColumnType("decimal(9,6)");

            builder.Property(p => p.Longitud)
                .HasColumnType("decimal(9,6)");

            builder.Property(p => p.FechaPublicacion)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Publicaciones)
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Material)
                .WithMany(m => m.Publicaciones)
                .HasForeignKey(p => p.IdMaterial)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
