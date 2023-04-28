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
    public async Task<Game> StartGame()
    {
        var newGame = new Game() { Money = 100_000, Bet = 0 };
        
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

        await _gameService.CreateAsync(newGame);
        await _deckService.CreateAsync(newDeck);

        return newGame;
    }

    [HttpGet("{id:length(24)}/next")]
    public async Task<ActionResult<Card>> Next(string id)
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



        return Ok(game);

//        var card = deck.Cards.First();
//        deck.Cards.Remove(card);
//        await _decksService.UpdateAsync(id, deck);

//        var score = GetScore(card);
//        switch (whom)
//        {
//            case "dealer":
//                {
//                    game.DealerScore += score; break;
//                }
//            case "player":
//                {
//                    if (score == 11 && game.PlayerScore + score > 21)
//                        game.PlayerScore += 1;
//                    else
//                        game.PlayerScore += score; break;
//                }
//        }

//        await _gamesService.UpdateAsync(id, game);

//        return card;
//    }

//    [HttpGet("{id:length(24)}/start/{bet}")]
//    public async Task<ActionResult<List<Card>>> StartGame(string id, int bet)
//    {
//        var game = await _gamesService.GetAsync(id);
//        if (game == null)
//        {
//            return NotFound();
//        }

//        var deck = new Deck() { Id = id };
//        deck.ShuffleDeck();
//        await _decksService.CreateAsync(deck);

//        game.Bet = bet;
//        game.PlayerTokens -= bet;
//        await _gamesService.UpdateAsync(id, game);

//#pragma warning disable CS8604 // ¬озможно, аргумент-ссылка, допускающий значение NULL.
//        List<Card> cards = new List<Card>()
//        {
//            DealCard(id, "player").Result.Value,
//            DealCard(id, "player").Result.Value,
//            DealCard(id, "dealer").Result.Value
//        };
//#pragma warning restore CS8604 // ¬озможно, аргумент-ссылка, допускающий значение NULL.

//        return cards;
//    }

//    [HttpGet("{id:length(24)}/end")]
//    public async Task<ActionResult<Game>> EndGame(string id)
//    {
//        var game = await _gamesService.GetAsync(id);
//        if (game == null)
//        {
//            return NotFound();
//        }

//        if (game.Bet == null)
//        {
//            return NotFound();
//        }

//        await _decksService.RemoveAsync(id);

//        CheckWinner(ref game);

//        game.Bet = null;
//        game.DealerScore = 0;
//        game.PlayerScore = 0;
//        await _gamesService.UpdateAsync(id, game);

//        return game;
//    }

//    public int GetScore(Card card)
//    {
//        if (int.TryParse(card.Rank, out int result)) return result;
//        if (card.Rank == "J" || card.Rank == "Q" || card.Rank == "K") return 10;
//        return 11;
//    }

//    public void CheckWinner(ref Game game)
//    {
//        if (game.PlayerScore > 21 && game.DealerScore > 21)
//        {
//            game.PlayerTokens += game.Bet;
//        }
//        else if (game.DealerScore > 21)
//        {
//            game.PlayerTokens += game.Bet * 2;
//        }
//        else if (game.PlayerScore > game.DealerScore)
//        {
//            game.PlayerTokens += game.Bet * 2;
//        }
//        else if (game.PlayerScore == game.DealerScore)
//        {
//            game.PlayerTokens += game.Bet;
//        }
    }
}