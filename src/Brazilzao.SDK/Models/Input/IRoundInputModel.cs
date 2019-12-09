using System;
using System.Collections.Generic;
using System.Text;

namespace Brazilzao.SDK.Models.Input
{
    public interface IRoundInputModel
    {
        Round Round { get; set; }

        int ChampionshipID { get; set; }
    }
}
