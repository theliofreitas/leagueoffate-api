using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace LeagueOfFateApi.Models
{
  public class Challenge {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string SummonerName { get; set; }

    public string SummonerId { get; set; }

    public string Status { get; set; }

    public long? MatchId { get; set; }
    
    public List<Criterial> Criterials { get; set; }
  }
}