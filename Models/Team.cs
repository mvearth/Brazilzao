using System.ComponentModel.DataAnnotations;

namespace Brazilzao.Models
{
    public class Team : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}