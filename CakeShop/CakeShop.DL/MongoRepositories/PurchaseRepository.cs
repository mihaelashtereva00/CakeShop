using CakeShop.DL.Interfaces;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.ModelsMongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeShop.DL.MongoRepositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<Purchase> _collection;
        IOptions<MongoDbConfiguration> _options;

        public PurchaseRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _collection = _database.GetCollection<Purchase>(_options.Value.PurcahsesCollection);
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesForClient(int clientId)
        {
            var result = _collection.Find(new BsonDocument()).ToEnumerable<Purchase>().Where(c => c.ClientId == clientId);
            return await Task.FromResult(result);
        }

        public async Task<Purchase> GetPurchasesById(Guid purchaseId)
        {
            var filter = Builders<Purchase>.Filter.Eq("Id", purchaseId);
            var purchase = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(purchase);
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesAfterDate(DateTime date)
        {
            var filter = Builders<Purchase>.Filter.Where(p => p.Date > date);
            var purchases = _collection.Find(filter).ToEnumerable();

            return await Task.FromResult(purchases);
        }

        public async Task<Purchase?> CreatePurchase(Purchase purchase)
        {
            _collection.InsertOne(purchase);

            return await Task.FromResult(purchase);
        }

        public async Task<Purchase> DeletePurchase(Guid purcahseId)
        {
            var purchase = await GetPurchasesById(purcahseId);
            var result = _collection.DeleteOne(x => x.Id == purcahseId);
            return await Task.FromResult(purchase);
        }

        public async Task<Purchase> UpdatePurchase(Purchase purcahse)
        {

            var filter = Builders<Purchase>.Filter.Eq("Id", purcahse.Id);
            var update = Builders<Purchase>.Update.Set(s => s.Cakes, purcahse.Cakes)
                                                  .Set(s => s.TotalMoney, purcahse.TotalMoney);

            _collection.UpdateOne(filter, update);

            return await Task.FromResult(purcahse);
        }

    }
}
