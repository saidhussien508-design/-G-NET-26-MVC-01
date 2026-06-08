using GemMangement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GemMangement
{
    public class GemAppDpContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.; Database = Gem; Trusted_Connection = True; TrustServerCertificate = True");
        }

        public DbSet<plane> planes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
