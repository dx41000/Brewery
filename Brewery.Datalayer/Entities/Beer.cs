using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brewery.Datalayer.Entities;

[Table("Beers")]
public class Beer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required] 
    public string Name { get; set; }
    [Required] 
    public decimal PercentageAlcoholByVolume { get; set; }

    public ICollection<Bar> Bars { get; set; }
    public ICollection<Brewery> Breweries{ get; set; }
}