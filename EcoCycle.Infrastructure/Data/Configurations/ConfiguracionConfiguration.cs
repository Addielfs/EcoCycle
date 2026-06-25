using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoCycle.Infrastructure.Data.Configurations
{
    public class ConfiguracionConfiguration : IEntityTypeConfiguration<Configuracion>
    {
        public void Configure(EntityTypeBuilder<Configuracion> builder)
        {
            builder.ToTable("Configuraciones");

            builder.HasKey(c => c.IdConfiguracion);

            builder.Property(c => c.Clave)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(c => c.Clave)
                .IsUnique();

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
