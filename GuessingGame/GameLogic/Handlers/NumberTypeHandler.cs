namespace GuessingGame.GameLogic.Handlers
{
    public class NumberTypeHandler : IGuessingTypeHandler
    {
        public bool IsGuessOutOfRange(string guess, object lowerBound, object upperBound)
        {
            return !(int.TryParse(guess, out int intGuess) && intGuess >= (int)lowerBound && intGuess <= (int)upperBound);
        }

        public object GenerateRandomValue(object min, object max)
        {
            return GenerateRandomNumber((int)min, (int)max + 1);
        }

        private int GenerateRandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
