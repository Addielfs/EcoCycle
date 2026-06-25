using EcoCycle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoCycle.Infrastructure.Data.Configurations
{
    public class RecompensaConfiguration : IEntityTypeConfiguration<Recompensa>
    {
        public void Configure(EntityTypeBuilder<Recompensa> builder)
        {
            builder.ToTable("Recompensas");

            builder.HasKey(r => r.IdRecompensa);

            builder.Property(r => r.Puntos)
                .IsRequired();

            builder.Property(r => r.FechaRegistro)
                .HasColumnType("date")
                .HasDefaultValueSql("CAST(GETUTCDATE() AS DATE)");

            builder.HasOne(r => r.Usuario)
                .WithMany(u => u.Recompensas)
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(r => r.IdPublicacion)
                .IsRequired(false);

            builder.HasOne(r => r.Publicacion)
                .WithMany()
                .HasForeignKey(r => r.IdPublicacion)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
