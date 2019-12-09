namespace Brazilzao.SDK.Models.Input
{
    public class DateInputModel : IDateInputModel
    {
        public int ChampionshipID { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
