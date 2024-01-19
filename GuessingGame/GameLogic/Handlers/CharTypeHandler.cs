namespace GuessingGame.GameLogic.Handlers
{
    public class CharTypeHandler : IGuessingTypeHandler
    {
        public bool IsGuessOutOfRange(string guess, object lowerBound, object upperBound)
        {
            return !(char.TryParse(guess, out char charGuess) && charGuess >= (char)lowerBound && charGuess <= (char)upperBound);
        }

        public object GenerateRandomValue(object min, object max)
        {
            return GenerateRandomLetter((char)min, (char)max);
        }

        private char GenerateRandomLetter(char min, char max)
        {
            return (char)new Random().Next(min, max + 1);
        }
    }
}
