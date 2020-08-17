using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LeagueOfFateApi.Services
{
  public class RiotService {
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

    public async Task<string> GetSummonerId(string summonerName) {
      var httpResponse = await _client.GetAsync($"{baseUrl}/summoner/v4/summoners/by-name/{summonerName}");
      var content = await httpResponse.Content.ReadAsStringAsync();

      dynamic responseObject = JToken.Parse(content);

      return responseObject.accountId;
    }
  }
}