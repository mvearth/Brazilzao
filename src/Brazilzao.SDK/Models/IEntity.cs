using System.ComponentModel.DataAnnotations.Schema;

namespace Brazilzao.SDK.Models
{
    public interface IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int Id { get; set; }
    }
}