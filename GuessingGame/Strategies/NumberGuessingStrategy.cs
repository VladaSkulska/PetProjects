namespace GuessingGame.Strategies
{
    public class NumberGuessingStrategy : IGuessingStrategy<int>
    {
        public GuessResult CheckGuess(string guess, int secretValue, int lowerBound, int upperBound)
        {
            int difference = Math.Abs(secretValue - int.Parse(guess));

            if (difference == 0)
            {
                return new GuessResult(isWin: true, distanceToWin: "");
            }
            if (difference <= 2)
            {
                return new GuessResult(isWin: false, distanceToWin: int.Parse(guess) < (int)secretValue
                    ? "- Very close but still low!\n"
                    : "- Very close but still high!\n");
            }
            else
            {
                return new GuessResult(isWin: false, distanceToWin: int.Parse(guess) < (int)secretValue
                    ? "- Too low!\n"
                    : "- Too high!\n");
            }
        }
    }
}