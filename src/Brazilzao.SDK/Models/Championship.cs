using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Brazilzao.SDK.Models
{
    public class Championship : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Edition { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InitialDate { get; set; }

        public int TeamVacancies { get; set; }

        public IList<Round> Rounds { get; set; }


        public IList<Round> GetRounds(IList<Team> teams)
        {
            if (teams.Count % 2 == 0)
                return this.GeneratePairRounds(teams);
            else
                return GenerateNotPairRounds(teams);
        }

        private IList<Round> GenerateNotPairRounds(IList<Team> teams)
        {
            var teamsCount = teams.Count;
            int roundsCount = teamsCount;
            int matchesPerRoundCount = teamsCount / 2;

            var firstTurn = new List<Round>();
            var secondTurn = new List<Round>();

            var lastRound = this.InitialDate;

            var nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
            var nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
            var nextDay = Math.Min(nextWednesday, nextSunday);

            lastRound = lastRound.AddDays(nextDay);

            for (int i = 0; i < teams.Count; i++)
            {
                firstTurn.Add(new Round()
                {
                    DateTime = lastRound,
                    Matches = new List<Match>(20)
                });

                nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
                nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
                nextDay = Math.Max(nextWednesday, nextSunday);

                lastRound = lastRound.AddDays(nextDay);
            }

            for (int i = 0; i < teams.Count; i++)
            {
                secondTurn.Add(new Round()
                {
                    DateTime = lastRound,
                    Matches = new List<Match>(20)
                });

                nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
                nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
                nextDay = Math.Max(nextWednesday, nextSunday);

                lastRound = lastRound.AddDays(nextDay);
            }

            for (int i = 0, k = 0; i < roundsCount; i++)
            {
                for (int j = -1; j < matchesPerRoundCount; j++)
                {
                    if (j >= 0)
                    {
                        firstTurn[i].Matches.Insert(j, new Match());
                        secondTurn[i].Matches.Insert(j, new Match());

                        firstTurn[i].Matches[j].Home = teams[k];
                        secondTurn[i].Matches[j].Visitor = teams[k];
                    }

                    k++;

                    if (k == roundsCount)
                        k = 0;
                }
            }

            int heighestTeam = teamsCount - 1;

            for (int i = 0, k = heighestTeam; i < roundsCount; i++)
            {
                for (int j = 0; j < matchesPerRoundCount; j++)
                {
                    firstTurn[i].Matches[j].Visitor = teams[k];
                    firstTurn[i].Matches[j].Home = teams[k];

                    k--;

                    if (k == -1)
                        k = heighestTeam;
                }
            }

            return firstTurn.Concat(secondTurn).ToList();
        }

        private IList<Round> GeneratePairRounds(IList<Team> teams)
        {
            int teamsCount = teams.Count;
            int roundsCount = teamsCount - 1;
            int matchsPerRoundCount = teamsCount / 2;

            var firstTurn = new List<Round>();
            var secondTurn = new List<Round>();

            var lastRound = this.InitialDate;

            var nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
            var nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
            var nextDay = Math.Min(nextWednesday, nextSunday);

            lastRound = lastRound.AddDays(nextDay);

            for (int i = 0; i < teams.Count; i++)
            {
                firstTurn.Add(new Round()
                {
                    DateTime = lastRound,
                    Matches = new List<Match>(20)
                });

                nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
                nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
                nextDay = Math.Max(nextWednesday, nextSunday);

                lastRound = lastRound.AddDays(nextDay);
            }

            for (int i = 0; i < teams.Count; i++)
            {
                secondTurn.Add(new Round()
                {
                    DateTime = lastRound,
                    Matches = new List<Match>(20)
                });

                nextWednesday = ((int)DayOfWeek.Wednesday - (int)lastRound.DayOfWeek + 7) % 7;
                nextSunday = ((int)DayOfWeek.Sunday - (int)lastRound.DayOfWeek + 7) % 7;
                nextDay = Math.Max(nextWednesday, nextSunday);

                lastRound = lastRound.AddDays(nextDay);
            }

            for (int i = 0, k = 0; i < roundsCount; i++)
            {
                for (int j = 0; j < matchsPerRoundCount; j++)
                {
                    firstTurn[i].Matches.Insert(j, new Match());
                    secondTurn[i].Matches.Insert(j, new Match());

                    firstTurn[i].Matches[j].Home = teams[k];
                    secondTurn[i].Matches[j].Visitor = teams[k];

                    k++;

                    if (k == roundsCount)
                        k = 0;
                }
            }

            for (int i = 0; i < roundsCount; i++)
            {
                if (i % 2 == 0)
                {
                    firstTurn[i].Matches[0].Visitor = teams[teamsCount - 1];
                    secondTurn[i].Matches[0].Home = teams[teamsCount - 1];
                }
                else
                {
                    firstTurn[i].Matches[0].Visitor = firstTurn[i].Matches[0].Home;
                    secondTurn[i].Matches[0].Home = firstTurn[i].Matches[0].Visitor;

                    firstTurn[i].Matches[0].Home =  teams[teamsCount - 1];
                }
            }

            int heighestTeam = teamsCount - 1;
            int heighestNotpairTeam = heighestTeam - 1;

            for (int i = 0, k = heighestNotpairTeam; i < roundsCount; i++)
            {
                for (int j = 1; j < matchsPerRoundCount; j++)
                {
                    firstTurn[i].Matches[j].Visitor = teams[k];
                    secondTurn[i].Matches[j].Home = teams[k];

                    k--;

                    if (k == -1)
                        k = heighestNotpairTeam;
                }
            }

            return firstTurn.Concat(secondTurn).ToList();
        }
    }
}