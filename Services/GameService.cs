using PockerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PockerApi.Services;

public class GameService
{
    private readonly IMongoCollection<Game> _gameCollection;

    //Connecting to Game table
    public GameService()
    {
        var mongoClient = new MongoClient(
            Environment.GetEnvironmentVariable("MONGODB_API"));

        var mongoDatabase = mongoClient.GetDatabase(
            "PockerDB");

        _gameCollection = mongoDatabase.GetCollection<Game>(
            "Game");
    }

    public async Task<List<Game>> GetAsync() =>
        await _gameCollection.Find(_ => true).ToListAsync();

    public async Task<Game?> GetAsync(string id) =>
        await _gameCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Game newGame) =>
        await _gameCollection.InsertOneAsync(newGame);

    public async Task UpdateAsync(string id, Game updatedGame) =>
        await _gameCollection.ReplaceOneAsync(x => x.Id == id, updatedGame);

    public async Task RemoveAsync(string id) =>
        await _gameCollection.DeleteOneAsync(x => x.Id == id);

    public async Task RemoveAllAsync() =>
        await _gameCollection.DeleteManyAsync(_ => true);
}