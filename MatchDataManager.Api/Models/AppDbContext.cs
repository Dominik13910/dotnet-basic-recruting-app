using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MatchDataManager.Api.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Location> Location { get; set; }
        public DbSet<Team> Team { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table requirements
            modelBuilder.Entity<Location>(eb =>
            {
                eb.HasKey(l => l.Id);
                eb.Property(l => l.Name).IsRequired().HasMaxLength(256);
                eb.Property(l => l.City).IsRequired().HasMaxLength(55);
                eb.HasIndex(l => l.Name).IsUnique();

            });

            modelBuilder.Entity<Team>(eb =>
            {
                eb.HasKey(t => t.Id);
                eb.Property(t => t.Name).IsRequired().HasMaxLength(256);
                eb.Property(t => t.CoachName).IsRequired().HasMaxLength(55);
                eb.HasIndex(t => t.Name).IsUnique();
            });
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
