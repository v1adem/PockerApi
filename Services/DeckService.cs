using MongoDB.Driver;
using PockerApi.Models;
using Microsoft.Extensions.Options;

namespace PockerApi.Services;

public class DeckService
{
    private readonly IMongoCollection<Deck> _deckCollection;

    //Connecting to Deck table
    public DeckService()
    {
        var mongoClient = new MongoClient(
            Environment.GetEnvironmentVariable("MONGODB_API"));

        var mongoDatabase = mongoClient.GetDatabase(
            "PockerDB");

        _deckCollection = mongoDatabase.GetCollection<Deck>(
            "Deck");
    }

    public async Task<List<Deck>> GetAsync() =>
        await _deckCollection.Find(_ => true).ToListAsync();

    public async Task<Deck?> GetAsync(string id) =>
        await _deckCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Deck newGame) =>
        await _deckCollection.InsertOneAsync(newGame);

    public async Task UpdateAsync(string id, Deck updatedGame) =>
        await _deckCollection.ReplaceOneAsync(x => x.Id == id, updatedGame);

    public async Task RemoveAsync(string id) =>
        await _deckCollection.DeleteOneAsync(x => x.Id == id);
}