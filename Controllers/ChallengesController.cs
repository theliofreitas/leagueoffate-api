using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LeagueOfFateApi.Services;
using System.Collections.Generic;
using LeagueOfFateApi.Models;

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
      var httpResponse = await _riotService.ValidateMatchId(challengeDTO.MatchId);
      
      if (!httpResponse.Value) {
        return httpResponse.Result;
      }

      // TODO: Validate Criterials

      return NoContent();
    }
  }
}