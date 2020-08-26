using MongoDB.Driver;
using System.Collections.Generic;
using LeagueOfFateApi.Models;

namespace LeagueOfFateApi.Services
{
  public class ChallengeService {
    private readonly IMongoCollection<Challenge> _challenges;

    public ChallengeService(IDatabaseSettings settings) {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      _challenges = database.GetCollection<Challenge>(settings.ChallengesCollectionName);
    }

    public List<Challenge> Get() {
      return _challenges.Find(challenge => true).ToList();
    }  

    public Challenge Get(string id) {
      return _challenges.Find<Challenge>(challenge => challenge.Id == id).FirstOrDefault();
    }

    public Challenge Create(Challenge challenge) {
      _challenges.InsertOne(challenge);
      return challenge;
    }

    public void Update(string id, Challenge challengeToUpdate) {
      _challenges.ReplaceOne(challenge => challenge.Id == id, challengeToUpdate);
    }
  }
}