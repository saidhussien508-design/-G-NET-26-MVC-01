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
    public  class TrainerConfigration:GemUserConfigration<Trainers>,IEntityTypeConfiguration<Trainers>
    {
        public new void Configure(EntityTypeBuilder<Trainers> builder)
        {
            builder.Property(s => s.CreatedAt).HasColumnName("HireDAte").HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
