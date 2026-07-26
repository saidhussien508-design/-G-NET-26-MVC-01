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
    public class BookingConfigration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(s => s.Id);
            builder.HasKey(s => new {s.Sessionid,s.Memberid});
            builder.Property(a => a.CreatedAt).HasColumnName("BookingDate").
              HasDefaultValueSql("GETDATE()");
        }
    }
}
