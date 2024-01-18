namespace GuessingGame.Interfaces
{
    public interface IGuessingStrategy
    {
        GuessResult CheckGuess(string guess, object secretValue, object lowerBound, object upperBound);
    }
}