using System.ComponentModel.DataAnnotations;

namespace Brewery.Models;

public class Beer
{
    [Required] 
    public string Name { get; set; }
    [Required] 
    public decimal PercentageAlcoholByVolume { get; set; }
    

}