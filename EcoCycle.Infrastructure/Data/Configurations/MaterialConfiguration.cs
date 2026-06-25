using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoCycle.Infrastructure.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materiales");

            builder.HasKey(m => m.IdMaterial);

            builder.Property(m => m.NombreMaterial)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(m => m.NombreMaterial)
                .IsUnique();

            builder.Property(m => m.Descripcion)
                .HasMaxLength(255);

            builder.HasMany(m => m.Publicaciones)
                .WithOne(p => p.Material)
                .HasForeignKey(p => p.IdMaterial)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
