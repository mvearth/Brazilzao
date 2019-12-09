using System;
using System.Collections.Generic;
using System.Text;

namespace Brazilzao.SDK.Models.Output
{
    public interface IClassificationOutputModel
    {
        int ChampionshipID { get; set; }

        int RoundID { get; set; }

        IList<TeamClassification> Classifications { get; set; }
    }
}
