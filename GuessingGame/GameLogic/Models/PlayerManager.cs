namespace GuessingGame.GameLogic.Models
{
    public class PlayerManager
    {
        private static int nextPlayerId = 0;

        public static Player LoadPlayer(string username)
        {
            return JsonDataHandler.LoadPlayerFromFile(username);
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

        private static int GetNextPlayerId()
        {
            return ++nextPlayerId;
        }
    }
}
