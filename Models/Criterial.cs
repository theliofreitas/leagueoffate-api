using System.ComponentModel.DataAnnotations;

namespace LeagueOfFateApi.Models 
{
  public class Criterial {
    [Required]
    public string Field { get; set; }

    [Required]
    public string Operator { get; set; }

    [Required]
    public string Value { get; set; }

    public bool Result { get; set; }
  }
}