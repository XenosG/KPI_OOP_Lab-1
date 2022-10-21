using System;

namespace Lab1
{
    public struct Game
    {
        private static uint constIndex = 0;
        public string FirstPlayer { get; }
        public string SecondPlayer { get; }
        public uint RatingCost { get; }
        public uint Index { get; }
        public bool Result { get; } //true == first player won, false == second player won

        public Game(string firstPlayer, string secondPlayer, uint cost, bool result)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            RatingCost = cost;
            Index = constIndex++;
            Result = result;
        }
    }

    public class GameAccount
    {
        public string UserName { get; }
        public uint CurrentRating { get; set; }
        public uint GamesCount { get; set; }

        public GameAccount(string name)
        {
            UserName = name;
            CurrentRating = 5;
            GamesCount = 0;
        }

        public void WinGame(GameAccount opponent, uint rating, List<Game> gameList, bool secondary)
        {
            if (!secondary)
            {
                gameList.Add(new Game(UserName, opponent.UserName, rating, true));
                opponent.LoseGame(this, rating, gameList, true);
            }
            CurrentRating += rating;
            GamesCount++;
        }

        public void LoseGame(GameAccount opponent, uint rating, List<Game> gameList, bool secondary)
        {
            if (!secondary)
            {
                gameList.Add(new Game(UserName, opponent.UserName, rating, false));
                opponent.WinGame(this, rating, gameList, true);
            }
            CurrentRating = CurrentRating > rating ? CurrentRating - rating : 1;
            GamesCount++;
        }

        public void GetStats(List<Game> gameList)
        {
            Console.WriteLine(String.Format("\n{0, 10}'s game history:\nOpponent   | Result  | Cost | Game Index", UserName));
            foreach (Game game in gameList)
                if (string.Equals(game.FirstPlayer, UserName) || string.Equals(game.SecondPlayer, UserName))
                    Console.WriteLine(String.Format("{0, -10} | {1,-7} | {2,4} | {3,10}", string.Equals(game.FirstPlayer, UserName) ? game.SecondPlayer : game.FirstPlayer, game.Result == (string.Equals(game.FirstPlayer, UserName) ? true : false) ? "Victory" : "Defeat", game.RatingCost, game.Index));
            Console.WriteLine("Current rating: " + CurrentRating + "\n");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            GameAccount playerOne = new GameAccount("Player One");
            GameAccount playerTwo = new GameAccount("Player Two");
            List<Game> gameHistory = new List<Game>();

            playerOne.WinGame(playerTwo, 2, gameHistory, false);
            playerOne.WinGame(playerTwo, 2, gameHistory, false);
            playerOne.WinGame(playerTwo, 2, gameHistory, false);
            playerOne.LoseGame(playerTwo, 2, gameHistory, false);
            playerOne.GetStats(gameHistory);
            playerTwo.GetStats(gameHistory);

        }
    }
}

