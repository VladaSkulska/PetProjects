namespace GuessingGame.GameLogic.Handlers
{
    public class NumberTypeHandler : IGuessingTypeHandler<int>
    {
        private int _lowerBound;
        private int _upperBound;

        public bool IsGuessOutOfRange(string guess, int lowerBound, int upperBound)
        {
            return !(int.TryParse(guess, out int intGuess) && intGuess >= lowerBound && intGuess <= upperBound);
        }

        public (int, int) GenerateRange()
        {
            _lowerBound = GenerateRandomNumber(1, 11);
            _upperBound = GenerateRandomNumber(11, 21);

            return (_lowerBound, _upperBound);
        }

        public (int LowerBound, int UpperBound) GetRange()
        {
            return (_lowerBound, _upperBound);
        }

        public int GenerateRandomValue(int min, int max)
        {
            return GenerateRandomNumber(min, max);
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
