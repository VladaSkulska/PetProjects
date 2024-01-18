namespace GuessingGame.GameLogic
{
    public class Game
    {
        private const int MAX_ATTEMPTS = 5;
        private int _attempts;
        private string _guessingCategory;
        private object _lowerBound;
        private object _upperBound;
        private object _guessingValue;
        private readonly IGuessingStrategy _guessingStrategy;

        public Game(IGuessingStrategy guessingStrategy, string guessingCategory)
        {
            _guessingStrategy = guessingStrategy;
            _guessingCategory = guessingCategory;
        }

        public string GuessingCategory => _guessingCategory;
        public int GetAttempts => _attempts;

        public (object, object) ResetGame(string category)
        {
            _attempts = 0;

            (_lowerBound, _upperBound) = GenerateRange(category);
            _guessingValue = GenerateRandomValue(_lowerBound, _upperBound);

            return (_lowerBound, _upperBound);
        }

        public GuessResult MakeGuess(string userGuess)
        {
            if (IsGuessOutOfRange(userGuess, _lowerBound, _upperBound))
            {
                return new GuessResult(isWin: false, distanceToWin: $"- Please enter a valid guess.\n");
            }

            GuessResult result = _guessingStrategy.CheckGuess(userGuess, _guessingValue, _lowerBound, _upperBound);

            if (result.IsWin)
            {
                return new GuessResult(isWin: true, distanceToWin: $"- Congrats! You guessed the correct value - [ {_guessingValue} ]\n");
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

        private (object, object) GenerateRange(string category)
        {
            if (category == "Number")
            {
                return (GenerateRandomNumber(1, 11), GenerateRandomNumber(11, 21));
            }
            else
            {
                return (GenerateRandomLetter('A', 'K'), GenerateRandomLetter('L', 'Z'));
            }
        }

        private bool IsGuessOutOfRange(string guess, object lowerBound, object upperBound)
        {
            if (lowerBound is int)
            {
                return !int.TryParse(guess, out int intGuess) || intGuess < (int)lowerBound || intGuess > (int)upperBound;
            }
            else if (lowerBound is char)
            {
                return !char.TryParse(guess, out char charGuess) || charGuess < (char)lowerBound || charGuess > (char)upperBound;
            }

            return true;
        }

        private object GenerateRandomValue(object min, object max)
        {
            if (min is int && max is int)
            {
                return GenerateRandomNumber((int)min, (int)max + 1);
            }
            else if (min is char && max is char)
            {
                return GenerateRandomLetter((char)min, (char)max);
            }

            return null;
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        private char GenerateRandomLetter(char min, char max)
        {
            return (char)new Random().Next(min, max + 1);
        }
    }
}