namespace Brazilzao.Models
{
    public class Match : IMatch
    {
        public ITeam Visitor { get; set; }

        public ITeam Home { get; set; }

        public int VisitorGoals { get; set; }

        public int HomeGoals { get; set; }

  

        public int GetVisitorPoints() => this.VisitorGoals > this.HomeGoals ? 3
            : this.HomeGoals == this.VisitorGoals ? 1
            : 0;

        public void SetResult(int homeGoals, int visitorGoals)
        {
            this.HomeGoals = homeGoals;
            this.VisitorGoals = visitorGoals;
        }
    }
}