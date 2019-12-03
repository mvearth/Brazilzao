using System.ComponentModel.DataAnnotations;

namespace Brazilzao.Models
{
    public class Match : IEntity
    {
        public int Id { get; set; }
        public Team Visitor { get; set; }
        public Team Home { get; set; }
        public int VisitorGoals { get; set; }
        public int HomeGoals { get; set; }
        
        public void SetResult(int homeGoals, int visitorGoals)
        {
            this.HomeGoals = homeGoals;
            this.VisitorGoals = visitorGoals;
        }
    }
}