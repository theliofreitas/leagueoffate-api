using Microsoft.AspNetCore.Mvc;
using LeagueOfFateApi.Services;
using System.Collections.Generic;
using LeagueOfFateApi.Models;

namespace LeagueOfFateApi.Controllers 
{
  [Route("api/[controller]")]
  [ApiController]
  public class ChallengesController : ControllerBase {
    private readonly ChallengeService _challengeService;

    public ChallengesController(ChallengeService challengeService) {
      _challengeService = challengeService;
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
    public ActionResult<Challenge> Create(Challenge challenge) {
      _challengeService.Create(challenge);

      return CreatedAtRoute("GetChallenge", new { id = challenge.Id.ToString() }, challenge);
    }
  }
}