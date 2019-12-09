using Brazilzao.SDK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            modelBuilder.Entity<Championship>().HasMany(c => c.Rounds);
            modelBuilder.Entity<TeamClassification>().HasOne(tc => tc.Team);
            modelBuilder.Entity<Match>().HasOne(tc => tc.Home);
            modelBuilder.Entity<Match>().HasOne(tc => tc.Visitor);
            modelBuilder.Entity<Round>().HasMany(r => r.Matches);

            modelBuilder.Entity<Championship>().ToTable("Championship");
            modelBuilder.Entity<TeamClassification>().ToTable("TeamClassification");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<Round>().ToTable("Round");
        }

        public void Populate()
        {
            var teams = this.CreateTeams();

            var championship = new Championship()
            {
                Edition = "First",
                InitialDate = DateTime.Now,
                Name = "Brasileirão",
                TeamVacancies = 20,
            };

            championship.Rounds = championship.GetRounds(teams);

            this.Add(championship);

            this.SaveChanges();
        }

        private IList<Team> CreateTeams()
        {
            return new List<Team>()
            {
                new Team() { Name = "Flamengo"},
                new Team() { Name = "Santos"},
                new Team() { Name = "Palmeiras"},
                new Team() { Name = "Grêmio"},
                new Team() { Name = "Athletico-PR"},
                new Team() { Name = "São Paulo"},
                new Team() { Name = "Corinthians"},
                new Team() { Name = "Internacional"},
                new Team() { Name = "Fortaleza"},
                new Team() { Name = "Goiás"},
                new Team() { Name = "Atlético-MG"},
                new Team() { Name = "Bahia"},
                new Team() { Name = "Vasco"},
                new Team() { Name = "Fluminense"},
                new Team() { Name = "Botafogo"},
                new Team() { Name = "Ceará"},
                new Team() { Name = "Cruzeiro"},
                new Team() { Name = "CSA"},
                new Team() { Name = "Chapecoense"},
                new Team() { Name = "Ava"}
            };
        }
    }
}