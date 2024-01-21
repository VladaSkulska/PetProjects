namespace GuessingGame.GameLogic
{
    public class Game<T>
    {
        private const int MAX_ATTEMPTS = 5;
        private int _attempts;
        private string _guessingCategory;
        private T _lowerBound;
        private T _upperBound;
        private T _guessingValue;
        private readonly IGuessingStrategy _guessingStrategy;
        private readonly IGuessingTypeHandler<T> _guessingTypeHandler;

        public Game(IGuessingStrategy guessingStrategy, string guessingCategory)
        {
            _guessingStrategy = guessingStrategy;
            _guessingCategory = guessingCategory;

            _guessingTypeHandler = CreateGuessingTypeHandler();
        }
        
        public string GuessingCategory => _guessingCategory;
        public int GetAttempts => _attempts;

        public (T, T) ResetGame(string category)
        {
            _attempts = 0;

            (_lowerBound, _upperBound) = GenerateRange(category);
            _guessingValue = _guessingTypeHandler.GenerateRandomValue(_lowerBound, _upperBound);

            return (_lowerBound, _upperBound);
        }

        public GuessResult MakeGuess(string userGuess)
        {
            if (_guessingTypeHandler.IsGuessOutOfRange(userGuess, _lowerBound, _upperBound))
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

        private IGuessingTypeHandler<T> CreateGuessingTypeHandler()
        {
            return _guessingCategory switch
            {
                "Number" => (IGuessingTypeHandler<T>)new NumberTypeHandler(),
                "Letter" => (IGuessingTypeHandler<T>)new CharTypeHandler(),
                _ => throw new InvalidOperationException($"Invalid guessing category: {_guessingCategory}"),
            };
        }

        private (T, T) GenerateRange(string category)
        {
            if (category == "Number")
            {
                _guessingTypeHandler.GenerateRange();
                var letterRange = _guessingTypeHandler.GetRange();
                return letterRange;
            }
            else if (category == "Letter")
            {
                _guessingTypeHandler.GenerateRange();
                var letterRange = _guessingTypeHandler.GetRange();
                return letterRange;
            }
            else
            {
                throw new InvalidOperationException($"Invalid guessing category: {category}");
            }
        }
    }
}
