using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeagueOfFateApi.Models
{
  public class ChallengeCreateDTO {
    [Required]
    public string SummonerName { get; set; }

    [Required, MinLength(1)]
    public List<Criterial> Criterials { get; set; }
  }
}