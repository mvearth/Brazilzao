namespace Brazilzao.Models
{
    public interface IMatch
    {
        ITeam Visitor { get; }

        ITeam Home { get; }

        int VisitorGoals { get; }

        int HomeGoals { get; }

        void SetResult(int homeGoals, int visitorGoals);
    }
}