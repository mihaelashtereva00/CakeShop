using CakeShop.Models.Models.Configurations;
using CakeShop.Models.ModelsMongoDB;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CakeShop.HealthChecks
{
    public class MongoHealthCheck : IHealthCheck
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<Purchase> _collection;
        IOptions<MongoDbConfiguration> _options;

        public MongoHealthCheck(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _collection = _database.GetCollection<Purchase>(_options.Value.PurcahsesCollection);
            }
            catch (MongoException e)
            {
                return HealthCheckResult.Unhealthy(e.Message);
            }

            return HealthCheckResult.Healthy("Mongo connection is OK");
        }
    }
}
