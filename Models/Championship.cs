using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Brazilzao.Models
{
    public class Championship : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public DateTime InitialDate { get; set; }
        public int TeamVacancies { get; set; }
        public IList<Round> Rounds { get; set; }
    }
}