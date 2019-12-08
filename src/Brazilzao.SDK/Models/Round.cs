using System;
using System.Collections.Generic;
using System.Linq;

namespace Brazilzao.SDK.Models
{
    public class Round : IEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public IList<TeamClassification> Classifications { get; set; } = new List<TeamClassification>();
        public IList<Match> Matches { get; set; } = new List<Match>();
        
        public void DistributePoints()
        {
            foreach (var match in this.Matches)
            {
                var home = Classifications.FirstOrDefault(c => c.Team.Equals(match.Home));
                home.UpdateWithResult(match.HomeGoals, match.VisitorGoals);

                var visitor = Classifications.FirstOrDefault(c => c.Team.Equals(match.Visitor));
                visitor.UpdateWithResult(match.VisitorGoals, match.HomeGoals);
            }
        }
    }
}