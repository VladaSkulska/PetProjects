namespace GuessingGame.GameLogic.Models
{
    public class GameResultEventArgs : EventArgs
    {
        public bool IsPlayerWin { get; set; }
    }
}