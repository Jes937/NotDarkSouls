using NotDarkSouls.Classes;
using NotDarkSouls.NPC.Friendly;

namespace NotDarkSouls.Classes.Map;

public static class StartingCity
{
    public static async Task<string> Visit(BaseClass player)
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to the city.");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Go to the forrest [1], " +
                "Talk to the guy [2]," +
                "Inventory [9], " +
                "Menu [0]"
            );

            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                return "Forrest";
            }
            else if (choice == "2")
            {
                var Bob = new Bob();
                Bob.Talk();
            }
            else if (choice == "9")
            {
                await Inventory.ShowInventory(player);
            }
            else if (choice == "0")
            {
                return "Menu";
            }
            else
            {
                Console.WriteLine(
                    "Please make a valid choice."
                );
            }
        }
    }
}