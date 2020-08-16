namespace LeagueOfFateApi.Models
{
  public class DatabaseSettings : IDatabaseSettings {
    public string ChallengesCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }

  public interface IDatabaseSettings {
    string ChallengesCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}