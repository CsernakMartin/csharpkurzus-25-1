using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace projekt
{
    class Menu
    {
        Auth auth = new Auth();
        public void mainMenu()
        {
            string[] choosableMenuPoints = { "Start game!", "Options", "Credit", "Exit game" };
            string choosedMenuPoint = menuTemplate(choosableMenuPoints);
            navigation(choosedMenuPoint);
        }
        public void navigation(string choosedMenuPoint)
        {
            if (choosedMenuPoint == "Start game!")
            {
                startGame();
            }
            if (choosedMenuPoint == "Options")
            {
                options();
            }
            if (choosedMenuPoint == "Credit")
            {
                try
                {
                    credit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load credits: " + ex.Message);
                }
            }
            if (choosedMenuPoint == "Exit game")
            {
                exit();
            }
        }
        public void credit()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Texts/CreditText.txt");
            string credits = File.ReadAllText(path, Encoding.UTF8);
            Console.Clear();
            Console.WriteLine(credits);
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            mainMenu();
            return;
        }
        public void options()
        {
            Console.Clear();
            Console.WriteLine("Work in progress!");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            mainMenu();
        }
        public void exit()
        {
            Console.Clear();
            Console.WriteLine("Exiting game...");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
        public void startGame()
        {
            Console.Clear();
            Game game = new Game();
            string choosedGameOption = menuTemplate(new string[] { "New game", "Load game", "Back" }, "Choose your game option:");
            if (choosedGameOption == "New game")
            {
                newGame(game);
            }

            if (choosedGameOption == "Load game")
            {
                loadGame(game);
            }
            if (choosedGameOption == "Back")
            {
                mainMenu();
            }
        }
        public string menuTemplate(string[] choosableMenuPoints, string text = "")
        {
            int selected = 0;
            int topVisible = 0;
            string choosedMenuPoint = "";
            ConsoleKey key;
            do
            {
                Console.Clear();
                if (!string.IsNullOrEmpty(text))
                {
                    Console.WriteLine(text);
                }
                for (int i = 0; i < choosableMenuPoints.Length && topVisible + i < choosableMenuPoints.Length; i++)
                {
                    int index = topVisible + i;
                    if (index == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(choosableMenuPoints[index]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    if (selected > 0)
                    {
                        selected--;
                        if (selected < topVisible)
                        {
                            topVisible--;
                        }
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (selected < choosableMenuPoints.Length - 1)
                    {
                        selected++;
                        if (selected >= topVisible + choosableMenuPoints.Length)
                        {
                            topVisible++;
                        }
                    }
                }
                else if (key == ConsoleKey.Enter)
                {
                    choosedMenuPoint = choosableMenuPoints[selected];
                }
            }
            while (key != ConsoleKey.Enter);
            return choosedMenuPoint;
        }
        public void newGame(Game game)
        {
            Player player = auth.register();
            string choosedGameMode = menuTemplate(new string[] { "Yes", "No" }, "Do you want tutorial?:");
            if (choosedGameMode == "Yes")
            {
                game.tutorial(player);
            }
            game.gameStart(player);
        }
        public void loadGame(Game game)
        {
            Player player = new();
            string filePath = "playerPoints.json";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Nincs mentett játék.");
                newGame(game);
            }
            try
            {

                string json = File.ReadAllText(filePath);
                var savedPlayers = JsonSerializer.Deserialize<List<DataForSave>>(json);
                string[] playerNames = savedPlayers
                .Select(p => $"{p.Name} - Point: {p.Points}, HP: {p.HP}, DMG: {p.Damage}")
                .Append("Back")
                .ToArray();
                string choosedSaveGame = menuTemplate(playerNames, "Choose save game:");
                if (choosedSaveGame == "Back")
                {
                    startGame();
                }
                int choosedSaveGameIndex = Array.IndexOf(playerNames, choosedSaveGame);
                var selectedSave = savedPlayers[choosedSaveGameIndex];
                player = new Player
                {
                    Name = selectedSave.Name,
                    Points = selectedSave.Points,
                    Damage = selectedSave.Damage,
                    HP = selectedSave.HP,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while loading saved game: " + ex.Message);
                Console.ReadKey();
            }
            game.gameStart(player);
        }
    }
}
