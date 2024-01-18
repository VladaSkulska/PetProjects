namespace GuessingGame
{
    public class Program
    {
        public static void Main()
        {
            Game game = CreateGameWithStrategy();

            PrintGreating(game, game.GuessingCategory);

            while (!game.IsGameOver())
            {
                Console.Write($"Attempt {game.GetAttempts + 1}: Make a guess : ");

                var userInput = Console.ReadLine();
                GuessResult result = game.MakeGuess(userInput);

                Console.WriteLine(result.DistanceToWin);

                if (result.IsWin || game.IsGameOver())
                {
                    AskForNewGame(ref game);
                }
            }
        }

        public static Game CreateGameWithStrategy()
        {
            Console.WriteLine("\n- Choose a category: \n");
            Console.WriteLine("1. Number");
            Console.WriteLine("2. Alphabet\n");

            int categoryChoice;
            while (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 1 || categoryChoice > 2)
            {
                Console.WriteLine("- Invalid choice. Please enter 1 or 2.");
            }

            IGuessingStrategy guessingStrategy;
            string guessingCategory;

            if (categoryChoice == 1)
            {
                guessingStrategy = new NumberGuessingStrategy();
                guessingCategory = "Number";
            }
            else
            {
                guessingStrategy = new AlphabetGuessingStrategy();
                guessingCategory = "Letter";
            }

            return new Game(guessingStrategy, guessingCategory);
        }

        public static void AskForNewGame(ref Game game)
        {
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();

            if (playAgainInput == "yes")
            {
                Console.Write("\nDo you want to change the guessing category? (yes/no): ");
                string changeCategoryInput = Console.ReadLine().ToLower();

                if (changeCategoryInput == "yes")
                {
                    game = CreateGameWithStrategy();
                }

                Console.Clear();
                PrintGreating(game, game.GuessingCategory);
            }
            else
            {
                Console.WriteLine("\nThanks for playing!");
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();

                Environment.Exit(0);
            }
        }

        public static void PrintGreating(Game game, string guessingCategory)
        {
            Console.Clear();
            Console.WriteLine($"--- Guess Random {guessingCategory} Game ---\n");

            (object lowerBound, object upperBound) = game.ResetGame(guessingCategory);
            Console.WriteLine($"- Please enter a value between {lowerBound} and {upperBound}.\n");
        }
    }
}
