namespace GuessingGame.GameLogic.Models
{
    public class GameResultEventArgs : EventArgs
    {
        public bool IsPlayerWin { get; set; }
    }

    public class Player
    {
        public event EventHandler<GameResultEventArgs> GameWon;
        public event EventHandler<GameResultEventArgs> GameDefeated;
        public int PlayerId { get; set; }
        public string Username { get; set; }
        public int GamesWon { get; set; }
        public int GamesDefeated { get; set; }

        public List<GameHistoryEntry> GameHistory { get; set; } = new List<GameHistoryEntry>();

        public void HandleWin()
        {
            GamesWon++;
            OnGameWon(new GameResultEventArgs { IsPlayerWin = true });
        }

        public void HandleLoss()
        {
            GamesDefeated++;
            OnGameDefeated(new GameResultEventArgs { IsPlayerWin = false });
        }

        private void OnGameWon(GameResultEventArgs e)
        {
            GameWon?.Invoke(this, e);
        }

        private void OnGameDefeated(GameResultEventArgs e)
        {
            GameDefeated?.Invoke(this, e);
        }
    }
}