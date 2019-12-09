using System;
using System.Collections.Generic;
using Brazilzao.API.Contexts;
using Brazilzao.SDK.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Brazilzao.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<BrazilzaoContext>();
                    if (context.Database.EnsureCreated())
                        Populate(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void Populate(BrazilzaoContext context)
        {
            var teams = CreateTeams();

            var championship = new Championship()
            {
                Edition = "First",
                InitialDate = DateTime.Now,
                Name = "Brasileirão",
                TeamVacancies = 20,
            };

            championship.Rounds = championship.GetRounds(teams);

            context.Add(championship);

            context.SaveChanges();
        }

        private static IList<Team> CreateTeams()
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
