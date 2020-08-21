using System.ComponentModel.DataAnnotations;

namespace LeagueOfFateApi.Models 
{
  public class Criterial {
    [Required]
    public string Field { get; set; }

    [Required]
    [RegularExpression(@"^[a-zA-Z_]*$", ErrorMessage="Please enter a valid value for the 'operator' field")]
    public string Operator { get; set; }

    [Required]
    [RegularExpression(@"^[A-Za-z0-9_]*$", ErrorMessage="Please enter a valid value for the 'value' field")]
    public string Value { get; set; }

    public bool Result { get; set; }
  }
}