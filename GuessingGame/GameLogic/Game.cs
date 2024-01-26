namespace GuessingGame.GameLogic
{
    public class Game<T>
    {
        private const int MAX_ATTEMPTS = 5;
        private int _attempts;
        private T _lowerBound;
        private T _upperBound;
        private T _guessingValue;
        private readonly IGuessingStrategy<T> _guessingStrategy;
        private readonly IGuessingTypeHandler<T> _guessingTypeHandler;
        private Player _currentPlayer;

        public Game(IGuessingStrategy<T> guessingStrategy, Player currentPlayer)
        {
            _guessingStrategy = guessingStrategy;
            _currentPlayer = currentPlayer;

            _guessingTypeHandler = CreateGuessingTypeHandler<T>();
        }
        
        public int GetAttempts => _attempts;
        public Player CurrentPlayer => _currentPlayer;

        public (T, T) ResetGame()
        {
            _attempts = 0;

            (_lowerBound, _upperBound) = _guessingTypeHandler.GenerateRange();
            _guessingValue = GenerateRandomValue(_lowerBound, _upperBound);

            _currentPlayer = PlayerManager.LoadPlayer(_currentPlayer.Username);

            return (_lowerBound, _upperBound);
        }

        public GuessResult MakeGuess(string userGuess)
        {
            if (IsGuessOutOfRange(userGuess, _lowerBound, _upperBound))
            {
                return new GuessResult(isWin: false, distanceToWin: $"- Please enter a valid guess.\n");
            }

            GuessResult result = _guessingStrategy.CheckGuess(userGuess, _guessingValue, _lowerBound, _upperBound);
            _attempts++;

            if (result.IsWin)
            {
                _currentPlayer.HandleWin();

                result.DistanceToWin = $"- Congrats! You guessed the correct value - [ {_guessingValue} ]\n";
            }
            else if (IsGameOver())
            {
                _currentPlayer.HandleLoss();

                result.DistanceToWin = $"- Game over! The correct value - [ {_guessingValue} ]\n";
            }

            return result;
        }

        public bool IsGameOver()
        {
            return _attempts >= MAX_ATTEMPTS;
        }

        private IGuessingTypeHandler<T> CreateGuessingTypeHandler<T>()
        {
            return (typeof(T) switch
            { 
                not null when typeof(T) == typeof(int) => new NumberTypeHandler() as IGuessingTypeHandler<T>,
                not null when typeof(T) == typeof(char) => new CharTypeHandler() as IGuessingTypeHandler<T>,
                _ => throw new InvalidOperationException($"Invalid guessing category: {typeof(T).Name}"),
            })!;
        }

        private bool IsGuessOutOfRange(string guess, T lowerBound, T upperBound)
        {
            return _guessingTypeHandler.IsGuessOutOfRange(guess, lowerBound, upperBound);
        }

        private T GenerateRandomValue(T min, T max)
        {
            return _guessingTypeHandler.GenerateRandomValue(min, max);
        }
    }
}
