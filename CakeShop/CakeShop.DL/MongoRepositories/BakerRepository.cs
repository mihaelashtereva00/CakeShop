using CakeShop.DL.Interfaces;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.Models.Requests;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CakeShop.Models.Models.Responses
{
    public class BakerRepository : IBakerRepository
    {
        MongoClient _dbClient;
        IMongoDatabase _database;
        IMongoCollection<Baker> _collection;
        IOptions<MongoDbConfiguration> _options;

        public BakerRepository(IOptions<MongoDbConfiguration> options)
        {
            _options = options;
            _dbClient = new MongoClient(_options.Value.ConnectionString);
            _database = _dbClient.GetDatabase(_options.Value.DatabaseName);
            _collection = _database.GetCollection<Baker>("Bakers");
        }

        public async Task<Baker> AddBaker(Baker baker)
        {
                var bakerNew = new Baker()
                {
                    Id = Guid.NewGuid(),
                    Name = baker.Name,
                    DateOfBirth = baker.DateOfBirth,
                    Age = baker.Age,
                    Specialty = baker.Specialty
                };

                _collection.InsertOne(bakerNew);

            return await Task.FromResult(bakerNew);
        }

        public async Task<Baker?> DeleteBaker(Guid id)
        {
            var baker = await GetBakertById(id);
            _collection.DeleteOne(x => x.Id == id);
            return await Task.FromResult(baker);
        }

        public async Task<Baker?> GetBakertById(Guid id)
        {
            var filter = Builders<Baker>.Filter.Eq("Id", id);
            var baker = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(baker);
        }

        public async Task<Baker?> GetBakertByName(string name)
        {
            var filter = Builders<Baker>.Filter.Eq("Name", name);
            var baker = _collection.Find(filter).FirstOrDefault();

            return await Task.FromResult(baker);
        }

        public async Task<IEnumerable<Baker>> GetAllBakers()
        {
            var bakers = _collection.Find(new BsonDocument()).ToEnumerable<Baker>();

            return await Task.FromResult(bakers);
        }

        public async Task<Baker> UpdateBaker(Baker bakerUpdated)
        {
            var filter = Builders<Baker>.Filter.Eq("Id", bakerUpdated.Id);
            var update = Builders<Baker>.Update.Set(s => s.Name, bakerUpdated.Name)
                                               .Set(s => s.DateOfBirth, bakerUpdated.DateOfBirth)
                                               .Set(s => s.Age, bakerUpdated.Age)
                                               .Set(s => s.Specialty, bakerUpdated.Specialty);

            _collection.UpdateOne(filter,update);

            return await Task.FromResult(bakerUpdated);
        }
    }
}
