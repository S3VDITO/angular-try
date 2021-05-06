namespace HeroesAPI.Models
{
    public class HeroesDatabaseSettings : IHeroesDatabaseSettings
    {
        public string HeroesCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
