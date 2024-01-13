using System;

namespace GuessNumberGame
{
    public class Program
    {
        public static void Main()
        {
            Game game = new();

            Greating(game);

            while (!game.IsGameOver())
            {
                Console.Write($"Attempt {game.GetAttempts + 1}: Make a guess : ");

                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int userGuess))
                {
                    GuessResult result = game.MakeGuess(userGuess);
                    Console.WriteLine(result.DistanceToWin);

                    if (result.IsWin || game.IsGameOver())
                    {
                        AskForNewGame(game);
                    }
                }
                else
                {
                    Console.WriteLine("- Invalid number. Please try again.\n");
                }
            }
        }

        public static void AskForNewGame(Game game)
        {
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();

            if (playAgainInput == "yes")
            {
                Console.Clear();
                Greating(game);
            }
            else
            {
                Console.WriteLine("\nThanks for playing!");
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();

                Environment.Exit(0);
            }
        }

        public static void Greating(Game game)
        {
            Console.WriteLine("--- Guess Random Number Game ---\n");

            (int lowerBound, int upperBound) = game.ResetGame();
            Console.WriteLine($"- Please enter a number between {lowerBound} and {upperBound}.\n");
        }
    }
}