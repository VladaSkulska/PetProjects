namespace GuessNumberGame
{
    public class Game
    {
        private const int MAX_ATTEMPTS = 5;

        private int _attempts;
        private int _lowerBound;
        private int _upperBound;
        private int _guessingNumber;
        
        public void ResetGame()
        {
            Console.Clear();
            _attempts = 0;

            Console.WriteLine("--- Guess Random Number Game ---");

            (_lowerBound, _upperBound) = GenerateRange();

            Console.WriteLine($"-- Range [ {_lowerBound} to {_upperBound} ] --\n");

            _guessingNumber = GenerateRandomNumber(_lowerBound, _upperBound + 1);
        }

        public void StartGame()
        {
            ResetGame();

            while (_attempts < MAX_ATTEMPTS)
            {
                Console.Write($"Attempt {_attempts + 1}: Make a guess : ");

                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int userGuess))
                {
                    if (IsGuessOutOfRange(userGuess, _lowerBound, _upperBound))
                    {
                        Console.WriteLine($"- Please enter a number between {_lowerBound} and {_upperBound}.\n");
                    }
                    else
                    {
                        GuessResult result = CheckGuess(userGuess, _guessingNumber);

                        if (result.IsWin)
                        {
                            Console.WriteLine($"- Congrats! You guessed the correct number - [ {_guessingNumber} ]\n");
                            AskForNewGame();
                        }
                        else
                        {
                            Console.WriteLine(result.DistanceToWin);
                        }

                        _attempts++;
                    }
                }
                else
                {
                    Console.WriteLine("- Invalid number. Please try again.\n");
                }
            }

            Console.WriteLine($"- Game over! You've reached the maximum number of attempts. " +
                                          $"The correct number was {_guessingNumber}.\n");

            AskForNewGame();
        }

        public void AskForNewGame()
        {
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();

            if (playAgainInput == "yes")
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("\nThanks for playing!");
                Console.WriteLine("Press any key to exit the application...");
                Console.ReadKey();

                Environment.Exit(0);
            }
        }

        public GuessResult CheckGuess(int guess, int secretNumber)
        {
            int difference = Math.Abs(secretNumber - guess);

            if (difference == 0)
            {
                return new GuessResult(isWin: true, distanceToWin: "");
            }
            if (difference <= 2)
            {
                return new GuessResult(isWin: false, distanceToWin: guess < secretNumber
                    ? "- Very close but still low!\n"
                    : "- Very close but still high!\n");
            }
            else
            {
                return new GuessResult(isWin: false, distanceToWin: guess < secretNumber
                    ? "- Too low!\n"
                    : "- Too high!\n");
            }
        }

        public (int, int) GenerateRange()
        {
            int lowerBound = GenerateRandomNumber(1, 11);
            int upperBound = GenerateRandomNumber(11, 21);

            return (lowerBound, upperBound);
        }

        public bool IsGuessOutOfRange(int guess, int lowerBound, int upperBound)
        {
            return guess < lowerBound || guess > upperBound;
        }

        public int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}