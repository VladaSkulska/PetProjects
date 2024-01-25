using System.Text.Json;

namespace GuessingGame.GameLogic.Handlers
{
    public class JsonDataHandler
    {
        private const string JsonFileName = "data.json";

        public static void SavePlayerToFile(Player player)
        {
            List<Player> players = LoadPlayersFromFile();
            Player existingPlayer = players.Find(u => u.PlayerId == player.PlayerId);

            if (existingPlayer != null)
            {
                existingPlayer.Username = player.Username;
                existingPlayer.GamesWon = player.GamesWon;
                existingPlayer.GamesDefeated = player.GamesDefeated;
                existingPlayer.GameHistory = player.GameHistory;
            }
            else
            {
                players.Add(player);
            }

            string json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFileName, json);
        }

        public static Player LoadPlayerFromFile(string username)
        {
            List<Player> players = LoadPlayersFromFile();
            return players.FirstOrDefault(u => u.Username == username) ?? new Player();
        }

        public static List<Player> LoadPlayersFromFile()
        {
            if (File.Exists(JsonFileName))
            {
                if(new FileInfo(JsonFileName).Length == 0)
                {
                    return new List<Player>();
                }
                else
                {
                    string json = File.ReadAllText(JsonFileName);
                    return JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
                }
            }
            return new List<Player>();
        }

        public static void UpdatePlayerGameHistory(Player player, bool isPlayerWin)
        {
            if (player.GameHistory == null)
            {
                player.GameHistory = new List<GameHistoryEntry>();
            }

            int gameNumber = player.GameHistory.Count + 1;

            player.GameHistory.Add(new GameHistoryEntry
            {
                GameNumber = gameNumber,
                IsPlayerWin = isPlayerWin
            });

            SavePlayerToFile(player);
        }
    }
}
