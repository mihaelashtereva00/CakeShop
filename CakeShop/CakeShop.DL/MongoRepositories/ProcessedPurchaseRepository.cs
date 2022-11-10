using CakeShop.DL.Interfaces;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.ModelsMongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeShop.DL.MongoRepositories
{
    public class ProcessedPurchaseRepository : IProcessedPurchasesRepository
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<Purchase> _collection;
        IOptions<MongoDbConfiguration> _options;

        public ProcessedPurchaseRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _collection = _database.GetCollection<Purchase>(_options.Value.ProcessedCollection);
        }

        public async Task<Purchase?> AddProcessedPurchase(Purchase purchase)
        {
            _collection.InsertOne(purchase);
            return await Task.FromResult(purchase);
        }

        public async Task<IEnumerable<Purchase>> GetAllProcessedPurchases()
        {
            var result = _collection.Find(new BsonDocument()).ToEnumerable();
            return await Task.FromResult(result);
        }

    }
}
