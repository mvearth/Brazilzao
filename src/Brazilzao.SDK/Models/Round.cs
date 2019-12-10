using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Brazilzao.SDK.Models
{
    public class Round : IEntity
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public IList<TeamClassification> Classifications { get; set; }

        public IList<Match> Matches { get; set; }
        
        public void DistributePoints()
        {
            foreach (var match in this.Matches)
            {
                var home = Classifications.FirstOrDefault(c => c.Team.Id.Equals(match.Home.Id));
                home.UpdateWithResult(match.HomeGoals, match.VisitorGoals);

                var visitor = Classifications.FirstOrDefault(c => c.Team.Id.Equals(match.Visitor.Id));
                visitor.UpdateWithResult(match.VisitorGoals, match.HomeGoals);
            }
        }
    }
}