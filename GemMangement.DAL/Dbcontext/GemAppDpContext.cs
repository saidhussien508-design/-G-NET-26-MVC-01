using GemMangement.DAL.Models;
using GemMangement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GemMangement.Pl.Dbcontext
{
    public class GemAppDpContext:DbContext
    {
        public GemAppDpContext(DbContextOptions<GemAppDpContext> option) : base(option)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server =.; Database = Gem; Trusted_Connection = True; TrustServerCertificate = True");
        //}

        public DbSet<plane> planes { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<MemberShip> memberShips { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<Trainers> trainers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
