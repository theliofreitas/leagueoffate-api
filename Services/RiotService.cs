using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace LeagueOfFateApi.Services
{
  public class RiotService : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client;
    private string baseUrl;
    private string riotToken;

    public RiotService(HttpClient client, IConfiguration configuration) {
      _configuration = configuration;

      baseUrl = _configuration["RiotAPISettings:BaseUrl"];
      riotToken = _configuration["RiotAPISettings:X-Riot-Token"];

      client.DefaultRequestHeaders.Add("X-Riot-Token", riotToken);
      _client = client;
    }

    public async Task<ActionResult<string>> GetSummonerId(string summonerName) {
      var httpResponse = await _client.GetAsync($"{baseUrl}/summoner/v4/summoners/by-name/{summonerName}");
      var content = await httpResponse.Content.ReadAsStringAsync();
      
      if (httpResponse.IsSuccessStatusCode) {
        var summonerObject = JObject.Parse(content);
        var summonerId = summonerObject.SelectToken("accountId").Value<string>();
        return summonerId;
      }
      else {
        switch ((int)httpResponse.StatusCode) {
          case 404:
            return NotFound(new { 
              status = "error", 
              message = "Summoner not found", 
              instructions = "Please send a valid summonerName"
            });
          default:
            return StatusCode(500, new {
              status = "error", 
              message = "Unable to return data from Riot Services", 
              instructions = "Please try again later"
            });
        }
      }
    }

    public async Task<ActionResult<bool>> ValidateMatchId(long matchId) {
      var httpResponse = await _client.GetAsync($"{baseUrl}/match/v4/matches/{matchId}");

      if (httpResponse.IsSuccessStatusCode) {
        return true;
      }
      else {
        switch ((int)httpResponse.StatusCode) {
          case 404:
            return NotFound(new { 
              status = "error", 
              message = "Match not found", 
              instructions = "Please send a valid and completed matchId"
            });
          default:
            return StatusCode(500, new {
              status = "error", 
              message = "Unable to return data from Riot Services", 
              instructions = "Please try again later"
            });
        }
      }
    }
  }
}