namespace GuessingGame.GameLogic.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Username { get; set; }
        public int GamesWon { get; set; }
        public int GamesDefeated { get; set; }

        public List<GameHistoryEntry> GameHistory { get; set; } = new List<GameHistoryEntry>();

        public void IncrementGamesWon()
        {
            ++GamesWon;
        }

        public void IncrementGamesDefeated()
        {
            ++GamesDefeated;
        }
    }
}