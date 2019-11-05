using System;
using System.Collections.Generic;
using System.Linq;

namespace Brazilzao.Models
{
    public class Round : IRound
    {
        public DateTime DateTime { get; set; }

        public IList<ITeamClassification> Classifications { get; set; }

        public IList<IMatch> Matches { get; set; }

        public void DistributePoints() 
        {
            foreach(var match in this.Matches){
                var home = Classifications.FirstOrDefault(c => c.Team.Equals(match.Home));

                home.Points += match.GetHomePoints();
                home.GoalsFor += match.HomeGoals;
                home.GoalsAgainst += match.VisitorGoals;
            }
        }
    }
}