using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using PockerApi.Models;
using PockerApi.Services;

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

        var newDeck = new Deck()
        {
            Id = newGame.Id
        };
        newDeck.ShuffleDeck();
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());
        newGame.Cards.Add(newDeck.NextCard());

        await _gameService.UpdateAsync(newGame.Id, newGame);

        await _deckService.CreateAsync(newDeck);

        return Ok(newGame);
    }

    [HttpGet("{id:length(24)}/next")]
    public async Task<ActionResult<Game>> Next(string id)
    {
        var deck = await _deckService.GetAsync(id);
        if (deck == null)
        {
            return NotFound();
        }

        var game = await _gameService.GetAsync(id);
        if (game == null)
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

    [HttpGet("{id:length(24)}/continue")]
    public async Task<ActionResult<Game>> Continue(string id)
    {
        var game = await _gameService.GetAsync(id);
        if (game == null) 
        { 
            return NotFound();
        }

        var newDeck = new Deck()
        {
            Id = game.Id
        };
        newDeck.ShuffleDeck();
        for(int i = 0; i < 5; i++)
        {
            game.Cards[i] = newDeck.NextCard();
        }

        await _gameService.UpdateAsync(game.Id, game);
        await _deckService.UpdateAsync(id, newDeck);

        return Ok(game);
    }
}

