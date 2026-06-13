using GemMangement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemMangement.DAL.Configration
{
    public class GemUserConfigration<t> : IEntityTypeConfiguration<t> where t : GemUser
    {
        public void Configure(EntityTypeBuilder<t> builder)
        {
            builder.Property(s => s.Name).
                HasColumnType("varchar").HasMaxLength(50);
            builder.Property(s=>s.Email).
                 HasColumnType("varchar").HasMaxLength(100);
            builder.Property(s => s.phone).
                 HasColumnType("varchar").HasMaxLength(11);
            builder .HasIndex (s=>s.phone).IsUnique ();
            builder.HasIndex(s => s.Email).IsUnique();
            builder.OwnsOne(s => s.Address, a =>
            {
                a.Property(o => o.City).HasColumnName("City").
                HasColumnType("varchar");
                a.Property(o => o.Street).HasColumnName("street").
                HasColumnType("varchar");
                a.Property(o => o.BuildingNumber).HasColumnName("BuildingNumber");
            });

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("EmailCheak", "Email Like'_%@_%_%'");
                tb.HasCheckConstraint("PhoneCheak",
                    "phone like '010%' or phone like '011%'or phone like '012%' or phone like '015%'");
            })
        }
    }
}
