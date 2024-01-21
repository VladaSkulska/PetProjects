namespace GuessingGame.GameLogic.Handlers
{
    public class CharTypeHandler : IGuessingTypeHandler<char>
    {
        private char _lowerBound;
        private char _upperBound;

        public bool IsGuessOutOfRange(string guess, char lowerBound, char upperBound)
        {
            return !(char.TryParse(guess, out char charGuess) && charGuess >= lowerBound && charGuess <= upperBound);
        }

        public (char, char) GenerateRange()
        {
            _lowerBound = GenerateRandomLetter('A', 'K');
            _upperBound = GenerateRandomLetter('L', 'Z');

            return (_lowerBound, _upperBound);
        }
        public (char LowerBound, char UpperBound) GetRange()
        {
            return (_lowerBound, _upperBound);
        }

        public char GenerateRandomValue(char min, char max)
        {
            return GenerateRandomLetter(min, max);
        }

        private char GenerateRandomLetter(char min, char max)
        {
            return (char)new Random().Next(min, max + 1);
        }
    }
}
