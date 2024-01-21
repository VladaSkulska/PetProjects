namespace GuessingGame.Interfaces
{
    public interface IGuessingTypeHandler<T>
    {
        bool IsGuessOutOfRange(string guess, T lowerBound, T upperBound);
        T GenerateRandomValue(T min, T max);
        (T, T) GenerateRange();
        (T LowerBound, T UpperBound) GetRange();
    }
}
