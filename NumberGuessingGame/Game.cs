using System;

namespace GuessNumberGame
{
    public class Game
    {
        private const int MAX_ATTEMPTS = 5;

        private int _attempts;
        private int _lowerBound;
        private int _upperBound;
        private int _guessingNumber;

        public int GetAttempts { get { return _attempts; } }

        public (int, int) ResetGame()
        {
            _attempts = 0;
            (_lowerBound, _upperBound) = GenerateRange();
            _guessingNumber = GenerateRandomNumber(_lowerBound, _upperBound + 1);

            return (_lowerBound, _upperBound);
        }

        public GuessResult MakeGuess(int userGuess)
        {
            if (IsGuessOutOfRange(userGuess, _lowerBound, _upperBound))
            {
                return new GuessResult(isWin: false, distanceToWin: $"- Please enter a number between {_lowerBound} and {_upperBound}.\n");
            }

            GuessResult result = CheckGuess(userGuess, _guessingNumber);

            if (result.IsWin)
            {
                return new GuessResult(isWin: true, distanceToWin: $"- Congrats! You guessed the correct number - [ {_guessingNumber} ]\n");
            }
            else
            {
                _attempts++;
                return result;
            }
        }

        public bool IsGameOver()
        {
            return _attempts >= MAX_ATTEMPTS;
        }

        private GuessResult CheckGuess(int guess, int secretNumber)
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

        private (int, int) GenerateRange()
        {
            int lowerBound = GenerateRandomNumber(1, 11);
            int upperBound = GenerateRandomNumber(11, 21);

            return (lowerBound, upperBound);
        }

        private bool IsGuessOutOfRange(int guess, int lowerBound, int upperBound)
        {
            return guess < lowerBound || guess > upperBound;
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
