namespace GuessingGame.Strategies
{
    public class AlphabetGuessingStrategy : IGuessingStrategy<char>
    {
        public GuessResult CheckGuess(string guess, char secretValue, char lowerBound, char upperBound)
        {
            if (secretValue.ToString() == guess)
            {
                return new GuessResult(isWin: true, distanceToWin: "");
            }
            else
            {
                int distance = Math.Abs(secretValue - char.Parse(guess));

                if (distance == 1 || distance == 2)
                {
                    return new GuessResult(isWin: false, distanceToWin: $"- Very close! {GetAlphabeticalHint(secretValue, char.Parse(guess))}\n");
                }
                else
                {
                    string hint = GetAlphabeticalHint(secretValue, char.Parse(guess));
                    return new GuessResult(isWin: false, distanceToWin: $"- Incorrect letter! {hint}\n");
                }
            }
        }

        private string GetAlphabeticalHint(char secretChar, char guessedChar)
        {
            string hint = secretChar < guessedChar ? "Try a letter closer to the beginning of the alphabet." : "Try a letter closer to the end of the alphabet.";

            return hint;
        }
    }
}