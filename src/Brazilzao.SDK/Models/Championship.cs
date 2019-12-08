using System;
using System.Collections.Generic;
using System.Linq;

namespace Brazilzao.SDK.Models
{
    public class Championship : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public DateTime InitialDate { get; set; }
        public int TeamVacancies { get; set; }
        public IList<Round> Rounds { get; set; }

        public IList<Round> GenerateRounds(IList<Team> teams)
        {
            var rounds = new List<Round>();

            var lastRound = this.InitialDate;

            for (int i = 0; i < teams.Count * 2 - 2; i++)
            {
                var nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
                var nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
                var nextDay = Math.Min(nextWednesday, nextSunday);

                rounds.Add(new Round()
                {
                    DateTime = lastRound.AddDays(nextDay),
                });
            }

            foreach (var home in teams)
            {
                var random = new Random();
                foreach (var visitor in teams.Except(new Team[] { home }).OrderBy(p => random.Next()))
                {
                    var round = rounds.FirstOrDefault(r => !r.Matches.Any(rm =>
                        rm.Home.Equals(home)
                         || rm.Visitor.Equals(home)
                          || rm.Home.Equals(visitor)
                           || rm.Visitor.Equals(visitor)));

                    round.Matches.Add(new Match() { Visitor = visitor, Home = home });
                }
            }

            return rounds;
        }
    }
}