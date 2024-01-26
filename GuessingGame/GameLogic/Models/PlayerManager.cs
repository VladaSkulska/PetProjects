namespace GuessingGame.GameLogic.Models
{
    public class PlayerManager
    {
        private static int nextPlayerId;

        static PlayerManager()
        {
            nextPlayerId = CalculateNextPlayerId();
        }

        private static int CalculateNextPlayerId()
        {
            List<Player> players = JsonDataHandler.LoadPlayersFromFile();

            if (players.Count > 0)
            {
                return players.Max(p => p.PlayerId) ;
            }

            return 0;
        }

        public static Player LoadPlayer(string username)
        {
            Player player = JsonDataHandler.LoadPlayerFromFile(username);

            player.GameWon += PlayerWon;
            player.GameDefeated += PlayerDefeated;

            return player;
        }
        public static List<Player> LoadAllPlayers()
        {
            return JsonDataHandler.LoadPlayersFromFile();
        }

        public static void SavePlayer(Player user)
        {
            JsonDataHandler.SavePlayerToFile(user);
        }

        public static Player CreateNewPlayer(string username)
        {
            Player newPlayer = new Player
            {
                PlayerId = GetNextPlayerId(),
                Username = username
            };

            Console.WriteLine($"- New user {newPlayer.Username} created!");

            return newPlayer;
        }

        public static void PlayerWon(object sender, GameResultEventArgs e)
        {
            JsonDataHandler.UpdatePlayerGameHistory((Player)sender, true);
        }

        public static void PlayerDefeated(object sender, GameResultEventArgs e)
        {
            JsonDataHandler.UpdatePlayerGameHistory((Player)sender, false);
        }

        private static int GetNextPlayerId()
        {
            return ++nextPlayerId;
        }
    }
}
