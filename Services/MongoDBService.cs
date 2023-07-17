using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NoticiaCadastroAPI.Model;

namespace NoticiaCadastroAPI.Services
{
    public class MongoDBService
    {

        private readonly IMongoCollection<PlayList> _playlistCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<PlayList>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<PlayList>> GetAsync() {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(PlayList playlist) {
            await _playlistCollection.InsertOneAsync(playlist);
            return;
        }
        public async Task AddToPlaylistAsync(string id, string movieId) {
            FilterDefinition<PlayList> filter = Builders<PlayList>.Filter.Eq("Id", id);
            UpdateDefinition<PlayList> update = Builders<PlayList>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;

        }
        public async Task DeleteAsync(string id) 
        {
            FilterDefinition<PlayList> filter = Builders<PlayList>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;
        }

    }
}
