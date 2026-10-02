using NotDarkSouls.DataBase;
using NotDarkSouls.Interfaces;

namespace NotDarkSouls.Classes.Map.Menu;

public enum StartAction { NewGame, Continue, Quit }

public record StartMenuResult(StartAction Action, PlayerDB? Player = null);

public class StartMenu
{
    private readonly IPlayer _playerService;

    public StartMenu(IPlayer playerService)
    {
        _playerService = playerService;
    }

    // Returns the chosen saved character, or null if the player wants a new game
    public async Task<StartMenuResult> StartAsync()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== NOT Dark Souls ===");
            Console.WriteLine("Continue [1], New game [2], Quit [0]");

            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                var player = await ChooseSavedCharacterAsync();

                if (player != null)
                    return new StartMenuResult(StartAction.Continue, player);
            }
            else if (choice == "2")
            {
                return new StartMenuResult(StartAction.NewGame);
            }
            else if (choice == "0")
            {
                return new StartMenuResult(StartAction.Quit);
            }
            else
            {
                Console.WriteLine("Please make a valid choice.");
            }
        }
    }

    public static async Task<string> Visit(BaseClass player)
    {
        return "Forrest";
    }

    private async Task<PlayerDB?> ChooseSavedCharacterAsync()
    {
        var players = await _playerService.GetAllAsync();

        if (players.Count == 0)
        {
            Console.WriteLine("No saved characters found. Start a new game instead.");
            return null;
        }

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Saved characters ===");

            for (int i = 0; i < players.Count; i++)
            {
                var p = players[i];
                Console.WriteLine($"[{i + 1}] {p.PlayerName} - {p.Class}, level {p.Level}");
            }

            Console.WriteLine("Back [0]");

            string? input = Console.ReadLine()?.Trim();

            if (input == "0")
                return null;

            if (int.TryParse(input, out int number) &&
                number >= 1 && number <= players.Count)
            {
                return players[number - 1];
            }

            Console.WriteLine("Please make a valid choice.");
        }
    }
}