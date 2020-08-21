using System.ComponentModel.DataAnnotations;

namespace LeagueOfFateApi.Models
{
  public class ChallengeValidateDTO {
    [Required]
    public long MatchId { get; set; }
  }
}