namespace GuessingGame
{
    public class Program
    {
        public static void Main()
        {
            var categoryChoice = ReadGameChoice();
            if (categoryChoice == 1)
            {
                RunGame(new NumberGuessingStrategy());
            }
            else
            {
                RunGame(new AlphabetGuessingStrategy());
            }
        }

        private static int ReadGameChoice()
        {
            Console.WriteLine("\n- Choose a category: \n");
            Console.WriteLine("1. Number");
            Console.WriteLine("2. Alphabet\n");

            int categoryChoice;
            while (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 1 || categoryChoice > 2)
            {
                Console.WriteLine("- Invalid choice. Please enter 1 or 2.");
            }

            return categoryChoice;
        }

        private static void RunGame<T>(IGuessingStrategy<T> strategy)
        {
            Game<T> game = new Game<T>(strategy);

            PrintGreating(game);

            while (!game.IsGameOver())
            {
                Console.Write($"Attempt {game.GetAttempts + 1}: Make a guess : ");

                var userInput = Console.ReadLine();
                GuessResult result = game.MakeGuess(userInput);

                Console.WriteLine(result.DistanceToWin);

                if (result.IsWin || game.IsGameOver())
                {
                    AskForNewGame();
                }
            }
        }

        private static void AskForNewGame()
        {
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();
            if (playAgainInput == "yes")
            {
                var choice = ReadGameChoice();
                if (choice == 1)
                {
                    RunGame(new NumberGuessingStrategy());
                }
                else
                {
                    RunGame(new AlphabetGuessingStrategy());
                }
            }
            else
            {
                Console.WriteLine("\nThanks for playing!");
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();

                Environment.Exit(0);
            }
        }

        private static void PrintGreating<T>(Game<T> game)
        {
            Console.Clear();
            Console.WriteLine($"--- Guess Random {typeof(T).Name} Game ---\n");

            var (lowerBound, upperBound) = game.ResetGame();
            Console.WriteLine($"- Please enter a value between {lowerBound} and {upperBound}.\n");
        }
    }
}
