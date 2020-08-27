using System;
using LeagueOfFateApi.Models;
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

    public JToken GetTeamData(JObject matchDetails, JToken participantData) {
      int allyTeamId = participantData.SelectToken("$.teamId").Value<int>();

      JToken teamData = matchDetails.SelectToken(
        "$.teams[?(@.teamId == " + allyTeamId + ")]"
      );

      return teamData;
    }

    public bool executeValidationLogic(Criterial criterial, JObject matchData) {
      if (criterial.Operator == "equal_to") {
        string matchValue = matchData.SelectToken(criterial.Field).Value<string>().ToLower();

        if (matchValue == criterial.Value) {
          return true;
        }
      }
      else if (criterial.Operator == "greater_than") {
        int matchValue = matchData.SelectToken(criterial.Field).Value<int>();

        if (matchValue >= Int32.Parse(criterial.Value)) {
          return true;
        }
      }
      else if (criterial.Operator == "lower_than") {
        int matchValue = matchData.SelectToken(criterial.Field).Value<int>();

        if (matchValue <= Int32.Parse(criterial.Value)) {
          return true;
        }
      }

      return false;
    }
  }
}