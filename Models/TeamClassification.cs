namespace Brazilzao.Models
{
    public class TeamClassification : ITeamClassification
    {
        public int Points { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int Draws { get; set; }

        public int Wins { get; set; }

        public int Loses { get; set; }

        public ITeam Team { get; set; }
        public void UpdateWithResult(int goalsFor, int goalsAgainst)
        {
            this.GoalsFor += goalsAgainst;
            this.GoalsAgainst += goalsAgainst;

            var matchPoints = this.CalculatePoints(goalsFor, goalsAgainst);

            this.UpdateMatchResult(matchPoints);

            this.Points += matchPoints;
        }

        public int CalculatePoints(int goalsFor, int goalsAgainst) => goalsFor > goalsAgainst ? 3
            : goalsFor == goalsAgainst ? 1
            : 0;

        private void UpdateMatchResult(int points)
        {
            switch (points)
            {
                case 3:
                    this.Wins++;
                    break;
                case 1:
                    this.Draws++;
                    break;
                case 0:
                    this.Loses++;
                    break;
            }
        }
    }
}