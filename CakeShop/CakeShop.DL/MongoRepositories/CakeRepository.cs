using CakeShop.DL.Interfaces;
using CakeShop.Models.Models;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.Models.Requests;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeShop.DL.MongoRepositories
{
    public class CakeRepository : ICakeRepository
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<Cake> _collection;
        IOptions<MongoDbConfiguration> _options;

        public CakeRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _collection = _database.GetCollection<Cake>("Cakes");
        }
        public async Task<Cake> AddCake(CakeRequest cakeRequest)
        {
            var cake = new Cake()
            {
                Id = Guid.NewGuid(),
                BakerId = cakeRequest.BakerId,
                Base = cakeRequest.Base,
                Form = cakeRequest.Form,
                Price = cakeRequest.Price,
                Topping = cakeRequest.Topping
            };

            _collection.InsertOne(cake);

            return await Task.FromResult(cake);
        }

        public async Task<Cake?> DeleteCake(Guid cakeId)
        {
            var cake = await GetCakeById(cakeId);
            _collection.DeleteOne(x => x.Id == cakeId);
            return await Task.FromResult(cake);
        }

        public async Task<IEnumerable<Cake>> GetAllCakes()
        {
            var cakes = _collection.Find(new BsonDocument()).ToEnumerable<Cake>();

            return await Task.FromResult(cakes);
        }

        public async Task<Cake?> GetCakeById(Guid id)
        {
            var filter = Builders<Cake>.Filter.Eq("Id", id);
            var cake = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(cake);
        }

        public async Task<Cake> UpdateCake(Cake cake)
        {
            var filter = Builders<Cake>.Filter.Eq("Id", cake.Id);
            var update = Builders<Cake>.Update.Set(s => s.BakerId, cake.BakerId)
                                               .Set(s => s.Price, cake.Price)
                                               .Set(s => s.Base, cake.Base)
                                               .Set(s => s.Form, cake.Form)
                                               .Set(s => s.Topping, cake.Topping);

            _collection.UpdateOne(filter, update);

            return await Task.FromResult(cake);
        }

    }
}
