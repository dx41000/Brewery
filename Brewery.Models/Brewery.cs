using System.ComponentModel.DataAnnotations;

namespace Brewery.Models;

public class Brewery
{
    [Required] 
    public string Name { get; set; }
    
    public IList<Beer>? Beers { get; set; }
}