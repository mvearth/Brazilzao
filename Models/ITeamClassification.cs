namespace Brazilzao.Models
{
    public interface ITeamClassification
    {
        ITeam Team { get; }

        int Points { get; set; }

        int GoalsFor { get; set; }

        int GoalsAgainst { get; set; }

        int Draws { get; set; }

        int Wins { get; set; }

        int Loses { get; set; }

        void UpdateWithResult(int goalsFor, int goalsAgainst);
    }
}