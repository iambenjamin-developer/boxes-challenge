using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Lead> Leads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Owned Types para evitar tablas extras
            modelBuilder.Entity<Lead>().OwnsOne(l => l.Contact);
            modelBuilder.Entity<Lead>().OwnsOne(l => l.Vehicle);

            base.OnModelCreating(modelBuilder);
        }
    }
}
