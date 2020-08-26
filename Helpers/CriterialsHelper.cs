using Newtonsoft.Json.Linq;

namespace LeagueOfFateApi.Helpers 
{
  public class CriterialsHelper {
    //TODO: Handle summonerId not found.
    public JToken GetParticipantData(JObject matchDetails, string summonerId) {
      byte participantId = matchDetails.SelectToken(
        "$.participantIdentities[?(@.player.summonerId == '" + summonerId + "')].participantId"
      ).Value<byte>();
      
      JToken participantData = matchDetails.SelectToken(
        "$.participants[?(@.participantId == " + participantId + ")]"
      );

      return participantData;
    }
  }
}