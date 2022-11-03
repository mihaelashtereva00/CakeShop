using CakeShop.DL.Interfaces;
using CakeShop.Models.Models;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.Models.Requests;
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
            _collection = _database.GetCollection<Purchase>("Purchase");
        }
        public async Task<IEnumerable<Purchase>> GetPurchases(Guid clientId)
        {
            var result = _collection.Find(new BsonDocument()).ToEnumerable<Purchase>();
            return await Task.FromResult(result);
        } 
        
        public async Task<Purchase> GetPurchasesById(Guid purchaseId)
        {
            var filter = Builders<Purchase>.Filter.Eq("Id", purchaseId);
            var purchase = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(purchase);
        }

        public async Task<Purchase?> CreatePurchase(PurchaseRequest purchaseRequest)
        {
            var document = new Purchase()
            {
                Id = Guid.NewGuid(),
                Cakes = purchaseRequest.Cakes,
                TotalMoney = purchaseRequest.TotalMoney,
                ClientId = purchaseRequest.ClientId
            };

            _collection.InsertOne(document);

            return await Task.FromResult(document);
        }

        public async Task<Purchase> DeletePurchase(Guid purcahseId)
        {
            var purchase = await GetPurchasesById(purcahseId);
            var result = _collection.DeleteOne(x => x.Id == purcahseId);
            return await Task.FromResult(purchase);
        }
    }
}
