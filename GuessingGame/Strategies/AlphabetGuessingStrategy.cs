namespace GuessingGame.Strategies
{
    public class AlphabetGuessingStrategy : IGuessingStrategy
    {
        public GuessResult CheckGuess(string guess, object secretValue, object lowerBound, object upperBound)
        {
            string secretLetter = secretValue.ToString().ToUpper();
            string guessedLetter = guess.ToUpper();

            if (secretLetter == guessedLetter)
            {
                return new GuessResult(isWin: true, distanceToWin: "");
            }
            else
            {
                int distance = Math.Abs(secretLetter[0] - guessedLetter[0]);

                if (distance == 1 || distance == 2)
                {
                    return new GuessResult(isWin: false, distanceToWin: $"- Very close! {GetAlphabeticalHint(secretLetter[0], guessedLetter[0])}\n");
                }
                else
                {
                    string hint = GetAlphabeticalHint(secretLetter[0], guessedLetter[0]);
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