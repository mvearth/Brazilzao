using System;
using System.Collections.Generic;

namespace Brazilzao.Models
{
    public interface IRound
    {
        DateTime DateTime { get; }

        IList<ITeamClassification> Classifications { get; }

        IList<IMatch> Matches { get; }
    }
}