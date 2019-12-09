using System;
using System.Collections.Generic;
using System.Text;

namespace Brazilzao.SDK.Models.Output
{
    public class ClassificationOutputModel : IClassificationOutputModel
    {
        public int ChampionshipID { get; set; }
        public int RoundID { get; set; }
        public IList<TeamClassification> Classifications { get; set; }
    }
}
