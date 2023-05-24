using NUnit.Framework;
using PockerApi.Models;

namespace PockerApi.UnitTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void CheckCombinationsTest_CombinationFlush()
        {
            Game game = new Game();
            game.Bet = 1;
            game.Cards = new List<Card>
            {
                new Card(1, 7),
                new Card(1, 5),
                new Card(1, 3),
                new Card(1, 8),
                new Card(1, 2)
            };

            Assert.AreEqual(1.75f, game.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationStraightFlush()
        {
            Game game = new Game();
            game.Bet = 1;
            game.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(1, 10),
                new Card(1, 13),
                new Card(1, 12),
                new Card(1, 11)
            };

            Assert.AreEqual(10f, game.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationFlushRoyal()
        {
            Game game = new Game();
            game.Bet = 1;
            game.Cards = new List<Card>
            {
                new Card(1, 14),
                new Card(1, 10),
                new Card(1, 13),
                new Card(1, 12),
                new Card(1, 11)
            };

            Assert.AreEqual(100f, game.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationOnePair()
        {
            Game game1 = new Game();
            game1.Bet = 1;
            game1.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 10),
                new Card(3, 9),
                new Card(4, 12),
                new Card(1, 11)
            };

            Game game2 = new Game();
            game2.Bet = 1;
            game2.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 9),
                new Card(3, 3),
                new Card(4, 10),
                new Card(1, 11)
            };

            Game game3 = new Game();
            game3.Bet = 1;
            game3.Cards = new List<Card>
            {
                new Card(1, 2),
                new Card(2, 12),
                new Card(3, 9),
                new Card(4, 11),
                new Card(1, 11)
            };

            Game game4 = new Game();
            game4.Bet = 1;
            game4.Cards = new List<Card>
            {
                new Card(1, 2),
                new Card(2, 10),
                new Card(3, 9),
                new Card(4, 11),
                new Card(1, 11)
            };

            Assert.AreEqual(1.25f, game1.CheckCombinations());
            Assert.AreEqual(1.25f, game2.CheckCombinations());
            Assert.AreEqual(1.25f, game3.CheckCombinations());
            Assert.AreEqual(1.25f, game4.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationTwoPairs()
        {
            Game game1 = new Game();
            game1.Bet = 1;
            game1.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 11),
                new Card(3, 12),
                new Card(4, 12),
                new Card(1, 11)
            };

            Game game2 = new Game();
            game2.Bet = 1;
            game2.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 9),
                new Card(3, 10),
                new Card(4, 10),
                new Card(1, 11)
            };

            Game game3 = new Game();
            game3.Bet = 1;
            game3.Cards = new List<Card>
            {
                new Card(1, 2),
                new Card(2, 11),
                new Card(3, 9),
                new Card(4, 11),
                new Card(2, 2)
            };

            Assert.AreEqual(1.4f, game1.CheckCombinations());
            Assert.AreEqual(1.4f, game2.CheckCombinations());
            Assert.AreEqual(1.4f, game3.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationThree()
        {
            Game game1 = new Game();
            game1.Bet = 1;
            game1.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 11),
                new Card(3, 3),
                new Card(4, 11),
                new Card(1, 11)
            };

            Game game2 = new Game();
            game2.Bet = 1;
            game2.Cards = new List<Card>
            {
                new Card(1, 10),
                new Card(2, 9),
                new Card(3, 10),
                new Card(4, 10),
                new Card(1, 11)
            };

            Game game3 = new Game();
            game3.Bet = 1;
            game3.Cards = new List<Card>
            {
                new Card(1, 2),
                new Card(2, 11),
                new Card(3, 9),
                new Card(4, 2),
                new Card(2, 2)
            };

            Assert.AreEqual(1.5f, game1.CheckCombinations());
            Assert.AreEqual(1.5f, game2.CheckCombinations());
            Assert.AreEqual(1.5f, game3.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationStraight()
        {
            Game game1 = new Game();
            game1.Bet = 1;
            game1.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 10),
                new Card(3, 8),
                new Card(4, 11),
                new Card(1, 12)
            };

            Assert.AreEqual(1.75f, game1.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationKare()
        {
            Game game1 = new Game();
            game1.Bet = 1;
            game1.Cards = new List<Card>
            {
                new Card(1, 9),
                new Card(2, 11),
                new Card(3, 11),
                new Card(4, 11),
                new Card(1, 11)
            };

            Game game2 = new Game();
            game2.Bet = 1;
            game2.Cards = new List<Card>
            {
                new Card(1, 10),
                new Card(2, 10),
                new Card(3, 10),
                new Card(4, 10),
                new Card(1, 11)
            };

            Assert.AreEqual(4f, game1.CheckCombinations());
            Assert.AreEqual(4f, game2.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationFullHouse()
        {
            Game game1 = new Game();
            game1.Bet = 1;
            game1.Cards = new List<Card>
            {
                new Card(1, 3),
                new Card(2, 11),
                new Card(3, 3),
                new Card(4, 11),
                new Card(1, 11)
            };

            Game game2 = new Game();
            game2.Bet = 1;
            game2.Cards = new List<Card>
            {
                new Card(1, 10),
                new Card(2, 11),
                new Card(3, 10),
                new Card(4, 10),
                new Card(1, 11)
            };

            Assert.AreEqual(2f, game1.CheckCombinations());
            Assert.AreEqual(2f, game2.CheckCombinations());
        }

        [Test]
        public void CheckCombinationsTest_CombinationNone()
        {
            Game game = new Game();
            game.Bet = 1;
            game.Cards = new List<Card>
            {
                new Card(1, 10),
                new Card(2, 11),
                new Card(3, 1),
                new Card(4, 2),
                new Card(1, 12)
            };

            Assert.AreEqual(0, game.CheckCombinations());
        }
    }
}