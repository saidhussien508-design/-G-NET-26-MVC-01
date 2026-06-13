using GemMangement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GemMangement.Pl.Dbcontext
{
    public class GemAppDpContext:DbContext
    {
        public GemAppDpContext(DbContextOptions<GemAppDpContext> option):base(option)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server =.; Database = Gem; Trusted_Connection = True; TrustServerCertificate = True");
        //}

        public DbSet<plane> planes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
