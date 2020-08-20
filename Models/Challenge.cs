using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeagueOfFateApi.Models
{
  public class Challenge {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required]
    public string SummonerName { get; set; }

    public string SummonerId { get; set; }

    public string Status { get; set; }

    public long? MatchId { get; set; }
    
    [Required, MinLength(1)]
    public List<Criterial> Criterials { get; set; }
  }
}