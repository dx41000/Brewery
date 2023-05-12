using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brewery.Datalayer.Entities;

[Table("Bars")]
public class Bar
{
 

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] 
    public string Name { get; set; }
    
    public ICollection<Beer> Beers { get; set; }
}