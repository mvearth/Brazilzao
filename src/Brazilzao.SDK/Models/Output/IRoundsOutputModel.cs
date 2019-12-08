using System.Collections.Generic;

namespace Brazilzao.SDK.Models.Output
{
    public interface IRoundsOutputModel
    {
        IList<Round> Rounds { get; set; }

        int ChampionshipID { get; set; }
    }
}
