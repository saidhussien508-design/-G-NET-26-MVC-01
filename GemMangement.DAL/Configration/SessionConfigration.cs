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
    public class SessionConfigration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(bt =>
            {
                bt.HasCheckConstraint("SessionCapasityCheak", "Capacity Between 1 and 25");
                bt.HasCheckConstraint("SessionDateCheak", "EndDate > StartDate");
            });
        }
    }
}
