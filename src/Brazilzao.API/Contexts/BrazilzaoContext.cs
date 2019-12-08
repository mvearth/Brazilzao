using Brazilzao.SDK.Models;
using Microsoft.EntityFrameworkCore;

namespace Brazilzao.API.Contexts
{
    public class BrazilzaoContext : DbContext
    {
        public BrazilzaoContext(DbContextOptions<BrazilzaoContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Championship> Championships { get; set; }

        public DbSet<TeamClassification> TeamClassifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Championship>().ToTable("Championship");
            modelBuilder.Entity<TeamClassification>().ToTable("TeamClassification");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<Round>().ToTable("Round");
        }
    }
}