namespace HeroesAPI.DAL
{
    using HeroesAPI.SettingsModels;

    using MongoDB.Driver;

    public class MongoDbContext
    {
        public MongoDbContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            Database = client.GetDatabase(databaseSettings.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
