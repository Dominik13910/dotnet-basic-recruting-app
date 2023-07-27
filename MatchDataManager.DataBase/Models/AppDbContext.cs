using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MatchDataManager.DataBase.Models
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Match> Match { get; set; }

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
            modelBuilder.Entity<Match>(eb =>
            {
                eb.HasKey(m => m.Id);
                eb.Property(m => m.FirstTeam).IsRequired();
                eb.Property(m => m.SecoundTeam).IsRequired();
                eb.Property(m => m.Location).IsRequired();
                eb.Property(m => m.StartData).IsRequired();
            });
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}