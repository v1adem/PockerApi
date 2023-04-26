using PockerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PockerApi.Services;

public class GameService
{
    private readonly IMongoCollection<Game> _gameCollection;

    public GameService(
        IOptions<PockerDBSettings> PockerDbSettings)
    {
        var mongoClient = new MongoClient(
            PockerDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            PockerDbSettings.Value.DatabaseName);

        _gameCollection = mongoDatabase.GetCollection<Game>(
            PockerDbSettings.Value.GameCollectionName);
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
}