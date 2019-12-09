using System;
using System.Collections.Generic;
using System.Text;

namespace Brazilzao.SDK.Models.Output
{
    public class RoundsOutputModel : IRoundsOutputModel
    {
        public IList<Round> Rounds { get; set; }
        public int ChampionshipID { get; set; }
    }
}
