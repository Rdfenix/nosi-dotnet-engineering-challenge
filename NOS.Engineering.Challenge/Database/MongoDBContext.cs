using MongoDB.Driver;
using NOS.Engineering.Challenge.Models;
using Microsoft.Extensions.Options;
using System;
using MongoDB.Bson;

namespace NOS.Engineering.Challenge.Database;
public class MongoDBContext
{
    private IMongoCollection<ContentMongo> _contents;


    public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase _database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _contents = _database.GetCollection<ContentMongo>(mongoDBSettings.Value.CollectionName);
    }


    public async Task<List<ContentMongo>> GetAsync()
    {
        return await _contents.Find(new BsonDocument()).ToListAsync();
    }
    public async Task CreateAsync(ContentMongo data)
    {
        await _contents.InsertOneAsync(data);
    }
    public async Task<ContentMongo> GetOneAsync(Guid id)
    {
        string ID = id.ToString();
        return (ContentMongo)await _contents.FindAsync(c => c.Id == ID);
    }
    /* public async Task AddToPlaylistAsync(string id, string movieId) {}
    public async Task DeleteAsync(string id) { }*/

}