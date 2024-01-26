namespace GuessingGame
{
    public class Program
    {
        public static void Main()
        {
            Player currentPlayer = null;

            DisplayMainMenu(currentPlayer);
        }

        private static void StartGame(Player currentPlayer)
        {
            var categoryChoice = ReadGameChoice();
            if (categoryChoice == 1)
            {
                RunGame(new NumberGuessingStrategy(), currentPlayer);
            }
            else
            {
                RunGame(new AlphabetGuessingStrategy(), currentPlayer);
            }
        }

        private static int ReadGameChoice()
        {
            ClearConsole();
            Console.WriteLine("- Choose a category: \n");
            Console.WriteLine("1. Number");
            Console.WriteLine("2. Alphabet\n");

            int categoryChoice;
            while (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 1 || categoryChoice > 2)
            {
                Console.WriteLine("- Invalid choice. Please enter 1 or 2.");
            }

            return categoryChoice;
        }

        private static void RunGame<T>(IGuessingStrategy<T> strategy, Player currentPlayer)
        {
            Game<T> game = new Game<T>(strategy, currentPlayer);

            PrintGreating(game);

            while (!game.IsGameOver())
            {
                Console.Write($"Attempt {game.GetAttempts + 1}: Make a guess : ");

                var userInput = Console.ReadLine();
                GuessResult result = game.MakeGuess(userInput);

                Console.WriteLine(result.DistanceToWin);

                if (result.IsWin || game.IsGameOver())
                {
                    AskForNewGame(currentPlayer);
                }
            }
        }

        private static void AskForNewGame(Player currentPlayer)
        {
            while (true)
            {
                Console.Write("Do you want to play again? (yes/no): ");
                string playAgainInput = Console.ReadLine().ToLower();

                if (playAgainInput == "yes")
                {
                    StartGame(currentPlayer);
                }
                else if (playAgainInput == "no")
                {
                    PrintMessage("\nThanks for playing! \nPress any key to return to the main menu...");
                    Console.ReadKey();
                    DisplayMainMenu(currentPlayer);
                }
                else
                {
                    Console.WriteLine("\n- Invalid input. Please enter 'yes' or 'no'.");
                }
            }
        }

        private static void DisplayMainMenu(Player currentPlayer)
        {
            while (true)
            {
                ClearConsole();

                Console.WriteLine("Main Menu:\n");
                Console.WriteLine("1. Choose or create player");
                Console.WriteLine("2. Start game");
                Console.WriteLine("3. Display statistics");
                Console.WriteLine("4. Exit\n");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("- Invalid choice. Please enter a number between 1 and 4.");
                }

                switch (choice)
                {
                    case 1:
                        currentPlayer = ChooseOrCreatePlayer();
                        break;

                    case 2:
                        if (currentPlayer != null)
                        {
                            StartGame(currentPlayer);
                        }
                        else
                        {
                            PrintMessage("\nPlease choose or create a player first.");
                            ClearConsole();
                        }
                        break;

                    case 3:
                        DisplayUserDataMenu(currentPlayer);
                        break;

                    case 4:
                        PrintMessage("\nThanks for playing! \nPress any key to exit...");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void DisplayUserDataMenu(Player currentPlayer)
        {
            ClearConsole();

            Console.WriteLine("- Choose an option: \n");
            Console.WriteLine("1. Display Game History for the current player");
            Console.WriteLine("2. Display User History for all players");
            Console.WriteLine("3. Display Overall Statistics");
            Console.WriteLine("4. Exit\n");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
            }

            switch (choice)
            {
                case 1:
                    DisplayCurrentPlayerHistory(currentPlayer);
                    break;

                case 2:
                    DisplayAllPlayersHistory();

                    break;

                case 3:
                    DisplayOverallStatistics();
                    break;

                case 4:
                    DisplayAllPlayersHistory();
                    break;

                case 5:
                    return;
            }
        }

        private static Player? ChooseOrCreatePlayer()
        {
            ClearConsole();
            Console.WriteLine("- Choose a category: \n");
            Console.WriteLine("1. Choose player");
            Console.WriteLine("2. Create or update player\n"); 

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                Console.WriteLine("- Invalid choice. Please enter 1 or 2.");
            }

            Player player;
            if (choice == 1)
            {
                player = ChoosePlayer();
                
                if (player == null)
                {
                    CreateOrUpdatePlayer();
                }
            }
            else
            {
                player = CreateOrUpdatePlayer();
            }

            return player;
        }

        private static Player? ChoosePlayer()
        {
            ClearConsole();
            Console.WriteLine("Existing players: \n");

            List<Player> allPlayers = PlayerManager.LoadAllPlayers();

            if (allPlayers.Count == 0)
            {
                PrintMessage("- No existing players found. \n\nRedirecting to create player. Press any key...");
                return null;
            }

            Console.WriteLine("Choose a player: \n");

            for (int i = 0; i < allPlayers.Count; i++)
            {
                Console.WriteLine($"- {i + 1}. {allPlayers[i].Username}");
            }

            Console.Write("\nEnter your choice: ");
            int playerChoice;
            while (!int.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 1 || playerChoice > allPlayers.Count)
            {
                Console.WriteLine($"- Invalid choice. Please enter a number between 1 and {allPlayers.Count}.");
            }

            Player selectedPlayer = allPlayers[playerChoice - 1];
            Console.WriteLine($"- Switched to player: {selectedPlayer.Username}");

            PrintMessage("\nPress any key to return...");
            return selectedPlayer;
        }

        private static Player CreateOrUpdatePlayer()
        {
            ClearConsole();
            Console.Write("Enter player username: ");
            string username = Console.ReadLine();

            Player player = PlayerManager.LoadPlayer(username);

            if (player.PlayerId == 0)
            {
                Console.WriteLine("- Player not found. Creating a new player...\n");
                Thread.Sleep(3000);
                player = PlayerManager.CreateNewPlayer(username);
            }
            else
            {
                Console.WriteLine($"- Player {player.Username} already exists. Updating player...");
            }

            PlayerManager.SavePlayer(player);
            Console.WriteLine($"- Player {player.Username} saved/updated.");

            PrintMessage("\nPress any key to return...");
            return player;
        }

        private static void DisplayCurrentPlayerHistory(Player player)
        {
            if (player == null)
            {
                PrintMessage("\nPlease choose or create a player first. \nPress any key to return...");
                return;
            }

            ClearConsole();
            Console.WriteLine($"\nGame History for Player {player.Username}: \n");

            var currentPlayer = PlayerManager.LoadPlayer(player.Username);
            foreach (var gameEntry in currentPlayer.GameHistory)
            {
                Console.Write($"| Game {gameEntry.GameNumber} - {(gameEntry.IsPlayerWin ? "Win" : "Defeat")} |\n");
            }
            Console.WriteLine();

            PrintMessage("Press any key to return...");
        }

        private static void DisplayAllPlayersHistory()
        {
            ClearConsole();
            Console.WriteLine("User History: \n");

            var players = PlayerManager.LoadAllPlayers();
            foreach (var player in players)
            {
                Console.Write($"- Player {player.Username} -  \n");
                foreach (var gameEntry in player.GameHistory)
                {
                    Console.Write($"| Game {gameEntry.GameNumber} - {(gameEntry.IsPlayerWin ? "Win" : "Defeat")} |\n");
                }
                Console.WriteLine();
            }

            PrintMessage("Press any key to return...");
        }

        private static void DisplayOverallStatistics()
        {
            ClearConsole();
            var allPlayers = PlayerManager.LoadAllPlayers();

            int totalGames = allPlayers.SelectMany(p => p.GameHistory).Count();
            int totalWins = allPlayers.Sum(p => p.GamesWon);
            int totalDefeats = allPlayers.Sum(p => p.GamesDefeated);

            Console.WriteLine($"Overall Statistics:");
            Console.WriteLine($"Total Games: {totalGames}");
            Console.WriteLine($"Total Wins: {totalWins}");
            Console.WriteLine($"Total Defeats: {totalDefeats}");
            Console.WriteLine($"Total Players: {allPlayers.Count}");
            Console.WriteLine();

            PrintMessage("Press any key to return...");
        }

        private static void PrintGreating<T>(Game<T> game)
        {
            Console.Clear();
            Console.WriteLine($"--- Guess Random {typeof(T).Name} Game ---\n");

            var (lowerBound, upperBound) = game.ResetGame();
            Console.WriteLine($"- ({game.CurrentPlayer.Username}), Please enter a value between {lowerBound} and {upperBound}.\n");
        }

        private static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine();
        }

        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
