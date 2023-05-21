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

        await _gameService.CreateAsync(newGame);
        if (newGame == null)
        {
            return BadRequest("Error creating object");
        }
   
        var newDeck = new Deck()
        {
            Id = newGame.Id
        };

        newDeck.ShuffleDeck();
        for (int i = 0; i < 5; i++) newGame.Cards.Add(newDeck.NextCard());
       
        await _gameService.UpdateAsync(newGame.Id, newGame);
        await _deckService.CreateAsync(newDeck);

        return Ok(newGame);
    }

    [HttpGet("/next")]
    public async Task<ActionResult<Game>> Next(Game game)
    {
        var deck = await _deckService.GetAsync(game.Id);
        if (deck == null)
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
    public async Task<ActionResult<Game>> Continue(Game game)
    {
        var newDeck = new Deck()
        {
            Id = game.Id
        };
        newDeck.ShuffleDeck();

        for (int i = 0; i < 5; i++)
        {
            game.Cards[i] = newDeck.NextCard();
        }

        await _gameService.UpdateAsync(game.Id, game);
        await _deckService.UpdateAsync(newDeck.Id, newDeck);
        
        return Ok(game);
    }

    [HttpDelete("/del")]
    public async Task<ActionResult> Wipe(string key)
    {
        if (key != Environment.GetEnvironmentVariable("DELETE_KEY"))
        {
            return BadRequest("Wrong delete password");
        }

        //Wiping DB
        await _gameService.RemoveAllAsync();
        await _deckService.RemoveAllAsync();
        
        return Ok();
    }
}

