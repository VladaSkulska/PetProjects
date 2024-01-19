namespace GuessingGame.Interfaces
{
    public interface IGuessingTypeHandler
    {
        bool IsGuessOutOfRange(string guess, object lowerBound, object upperBound);
        object GenerateRandomValue(object min, object max);
    }
}
