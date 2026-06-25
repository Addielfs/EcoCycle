using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoCycle.Infrastructure.Data.Configurations
{
    public class CentroReciclajeConfiguration : IEntityTypeConfiguration<CentroReciclaje>
    {
        public void Configure(EntityTypeBuilder<CentroReciclaje> builder)
        {
            builder.ToTable("CentrosReciclaje");

            builder.HasKey(c => c.IdCentro);

            builder.Property(c => c.NombreCentro)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Direccion)
                .HasMaxLength(255);

            builder.Property(c => c.Latitud)
                .HasColumnType("decimal(9,6)");

            builder.Property(c => c.Longitud)
                .HasColumnType("decimal(9,6)");

            builder.Property(c => c.Telefono)
                .HasMaxLength(20);
        }
    }
}
