using System.ComponentModel.DataAnnotations;

namespace Brewery.Models;

public class Bar
{
    [Required] 
    public string Name { get; set; }
    
    public IList<Beer>? Beers { get; set; }
}