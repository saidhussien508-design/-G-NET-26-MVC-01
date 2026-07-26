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
    public class CategoreConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(s => s.CaregoryNAme).
                HasColumnType("varchar").HasMaxLength(20);

            builder.HasData(
                new Category { Id = 1, CaregoryNAme = "Cardio" },
                 new Category { Id = 2, CaregoryNAme = "strength" },
                  new Category { Id = 3, CaregoryNAme = "yoga" },
                   new Category { Id = 4, CaregoryNAme = "Boxing" },
                    new Category { Id = 5, CaregoryNAme = "crossfit" }
            );
        }
    }
}
