namespace Brazilzao.SDK.Models.Input
{
    public interface IDateInputModel
    {
        int ChampionshipID { get; set; }

        int Day { get; set; }

        int Month { get; set; }

        int Year { get; set; }
    }
}
