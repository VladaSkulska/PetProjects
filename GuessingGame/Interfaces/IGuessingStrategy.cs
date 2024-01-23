namespace GuessingGame.Interfaces
{
    public interface IGuessingStrategy<in T>
    {
        GuessResult CheckGuess(string guess, T secretValue, T lowerBound, T upperBound);
    }
}