using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using PockerApi.Models;
using PockerApi.Services;
//using Newtonsoft.Json;

namespace BlackJackApi.Controllers;

[ApiController]
[Route("pocker")]
public class PockerController : ControllerBase
{
    private readonly GameService _gameService;
    private readonly DeckService _deckService;

    public PockerController(GameService gameService, DeckService deckService)
    {
        _gameService = gameService;
        _deckService = deckService;
    }

    [HttpGet]
    public async Task<ActionResult<Game>> StartGame()
    {
        var newGame = new Game() { Money = 100_000, Bet = 0 };
        var newDeck = new Deck()

        try
        {
            await _gameService.CreateAsync(newGame);
        }
        catch
        {
            return BadRequest("Error creating object");
        }
        {
            Id = newGame.Id
        };

        newDeck.ShuffleDeck();
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());

        try
        {
            await _gameService.UpdateAsync(newGame.Id, newGame);
        }
        catch 
        {
            return BadRequest("Error updating object")
        }

        try
        {
            await _deckService.CreateAsync(newDeck);
        }
        catch
        {
            return BadRequest("Error creating object");
        }

        return Ok(newGame);
    }

    [HttpGet("/next")]
    public async Task<ActionResult<Game>> Next(string id)
    {
        try
        {
            var deck = await _deckService.GetAsync(id);
            var game = await _gameService.GetAsync(id);
        }
        catch
        {
            return NotFound();
        }
        
        // Change unblock cards
        for (int i = 0; i < 5; i++)
        {
            if (!game.Cards[i].IsBlocked)
            {
                game.Cards[i] = deck.NextCard();
            }
        }

        // Checking combinations
        game.Money = game.CheckCombinations() * game.Bet;
        game.Bet = 0;

        return Ok(game);
    }

    [HttpGet("/continue")]
    public async Task<ActionResult<Game>> Continue(Game jsonGame)
    {
        try
        {
            var game = jsonGame;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
        
        //Creating new Deck
        var newDeck = new Deck()
        {
            Id = game.Id
        };
        newDeck.ShuffleDeck();
        for (int i = 0; i < 5; i++)
        {
            game.Cards[i] = newDeck.NextCard();
        }

        //Updating current game properties
        try
        {
            await _gameService.UpdateAsync(game.Id, game);
            await _deckService.UpdateAsync(id, newDeck);
        }
        catch () 
        {
            return BadRequest("Error updating object");
        }
        

        return Ok(game);
    }

    [HttpDelete]
    {
    public async Task<ActionResult> Continue(string key)
    {
        if (key != Environment.GetEnvironmentVariable("DELETE_KEY"))
        {
            return BadRequest("Wrong delete password");
        }

        try
        {
            await _gameService.RemoveAllAsync();
            await _deckService.RemoveAllAsync();
        }
        catch(Exception ex)
        { 
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
}

