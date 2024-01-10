using System;

namespace GuessNumberGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--- Guess Random Number Game ---");

            int lowerBound = GenerateRandomNumber(1, 11);
            int upperBound = GenerateRandomNumber(11, 21);

            Console.WriteLine($"-- Range [ {lowerBound} to {upperBound} ] --\n");

            int guessingNumber = GenerateRandomNumber(lowerBound, upperBound + 1);

            PlayGame(lowerBound, upperBound, guessingNumber);
        }

        static void PlayGame(int lowerBound, int upperBound, int guessingNumber)
        {
            while (true)
            {
                Console.Write("Make a guess : ");

                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int userGuess))
                {
                    if (IsGuessOutOfRange(userGuess, lowerBound, upperBound))
                    {
                        Console.WriteLine($"- Please enter a number between {lowerBound} and {upperBound}.\n");
                    }
                    else if (userGuess == guessingNumber)
                    {
                        Console.WriteLine($"- Congrats! You guessed the correct number - [ {guessingNumber} ]\n");
                        break;
                    }
                    else
                    {
                        ProvideFeedback(userGuess, guessingNumber);
                    }
                }
                else
                {
                    Console.WriteLine("- Invalid number. Please try again.\n");
                }
            }
        }

        static bool IsGuessOutOfRange(int guess, int lowerBound, int upperBound)
        {
            return guess < lowerBound || guess > upperBound;
        }

        static void ProvideFeedback(int guess, int secretNumber)
        {
            int difference = Math.Abs(secretNumber - guess);

            if (difference <= 2)
            {
                Console.WriteLine(guess < secretNumber
                    ? "- Very close but still low!\n"
                    : "- Very close but still high!\n");
            }
            else
            {
                Console.WriteLine(guess < secretNumber ? "- Too low!\n" : "- Too high!\n");
            }
        }

        static int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}