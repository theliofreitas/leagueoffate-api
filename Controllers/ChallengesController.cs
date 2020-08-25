using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LeagueOfFateApi.Services;
using System.Collections.Generic;
using LeagueOfFateApi.Models;
using Newtonsoft.Json.Linq;

namespace LeagueOfFateApi.Controllers 
{
  [Route("api/[controller]")]
  [ApiController]
  public class ChallengesController : ControllerBase {
    private readonly ChallengeService _challengeService;
    private readonly RiotService _riotService;

    public ChallengesController(ChallengeService challengeService, RiotService riotService) {
      _challengeService = challengeService;
      _riotService = riotService;
    }

    [HttpGet]
    public ActionResult<List<Challenge>> Get() {
      return _challengeService.Get();
    }

    [HttpGet("{id:length(24)}", Name="GetChallenge")]
    public ActionResult<Challenge> Get(string id) {
      var challenge = _challengeService.Get(id);

      if (challenge == null) {
        return NotFound();
      }

      return challenge;
    }

    [HttpPost]
    public async Task<ActionResult<Challenge>> Create(ChallengeCreateDTO challengeDTO) {
      var httpResponse = await _riotService.GetSummonerId(challengeDTO.SummonerName);

      Challenge challenge = new Challenge{
        SummonerName = challengeDTO.SummonerName,
        Criterials = challengeDTO.Criterials
      };

      if (httpResponse.Value != null) {
        challenge.SummonerId = httpResponse.Value;
      }
      else {
        return httpResponse.Result;
      }

      challenge.Status = "open";
      _challengeService.Create(challenge);

      return CreatedAtRoute("GetChallenge", new { id = challenge.Id.ToString() }, challenge);
    }
  
    [HttpPatch("{id:length(24)}")]
    public async Task<IActionResult> Validate(string id, ChallengeValidateDTO challengeDTO) {
      var httpResponse = await _riotService.GetMatchDetails(challengeDTO.MatchId);
      
      if (httpResponse.Value == null) {
        return httpResponse.Result;
      }

      JObject matchDetails = httpResponse.Value;
      Challenge challenge = _challengeService.Get(id);

      // TODO: Validate Criterials
      var test = ValidateCriterials(challenge, matchDetails);

      return NoContent();
    }

    // TODO: Break this code into functions
    private bool ValidateCriterials(Challenge challenge, JObject matchDetails) {
      int participantIndex = 0;
      int allyTeamIndex = 0;

      // Transforming matchDetails into criterial parts
      JObject match = new JObject();
      JObject participant = new JObject();
      JObject allyTeam = new JObject();

      match.Add("match", matchDetails);
      participant.Add("participant", matchDetails["participants"][participantIndex]);
      allyTeam.Add("team", matchDetails["teams"][allyTeamIndex]);
      
      // Examples
      var largestKillingSpree = participant.SelectToken("participant.stats.largestKillingSpree");
      var teamWin = allyTeam.SelectToken("team.win");
      var gameMode = match.SelectToken("match.gameMode");

      return false;
    }
  }
}