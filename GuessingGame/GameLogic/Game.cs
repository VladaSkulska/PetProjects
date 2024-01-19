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
        private readonly IGuessingTypeHandler _guessingTypeHandler;

        public Game(IGuessingStrategy guessingStrategy, string guessingCategory)
        {
            _guessingStrategy = guessingStrategy;
            _guessingCategory = guessingCategory;

            _guessingTypeHandler = CreateGuessingTypeHandler();
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

        private IGuessingTypeHandler CreateGuessingTypeHandler()
        {
            return _guessingCategory switch
            {
                "Number" => new NumberTypeHandler(),
                "Letter" => new CharTypeHandler(),
                _ => throw new InvalidOperationException($"Invalid guessing category: {_guessingCategory}"),
            };
        }

        private (object, object) GenerateRange(string category)
        {
            if (category == "Number")
            {
                return (_guessingTypeHandler.GenerateRandomValue(1, 11), _guessingTypeHandler.GenerateRandomValue(11, 21));
            }
            else if (category == "Letter")
            {
                return (_guessingTypeHandler.GenerateRandomValue('A', 'K'), _guessingTypeHandler.GenerateRandomValue('L', 'Z'));
            }
            else
            {
                throw new InvalidOperationException($"Invalid guessing category: {category}");
            }
        }

        private bool IsGuessOutOfRange(string guess, object lowerBound, object upperBound)
        {
            return _guessingTypeHandler.IsGuessOutOfRange(guess, lowerBound, upperBound);
        }

        private object GenerateRandomValue(object min, object max)
        {
            return _guessingTypeHandler.GenerateRandomValue(min, max);
        }
    }
}
