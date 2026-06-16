using GemMangement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GemMangement.Configration
{
    public class PlaneConfigration : IEntityTypeConfiguration<plane>
    {
        public void Configure(EntityTypeBuilder<plane> builder)
        {
            builder.HasKey("Id");

            builder.Property(s => s.Name)
                .HasColumnType("varchar").
                HasMaxLength(50);

            builder.Property(s => s.Description).
                HasMaxLength(200);

            builder.Property(s => s.Price)
                .HasPrecision(10, 3);

            builder.Property(s => s.CreatedAt).
                HasDefaultValueSql("GETDATE()");

            builder.ToTable(s => s.HasCheckConstraint("planDuration", "DurationDate Between 1 and 365"));
        }
    }
}
