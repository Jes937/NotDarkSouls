using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public static class Forrest
{
    public static async Task<string> Visit(BaseClass player)
    {
        Console.WriteLine();
        Console.WriteLine(
            "Welcome you are now standing in the forrest of the forbiden"
        );

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Explore the forrest [1], " +
                "Go to the nearest city [2], " +
                "Inventory [9], " +
                "Menu [0]"
            );

            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                Console.WriteLine();
                Console.WriteLine("You explore the forrest.");

                // Add exploration/combat here later.
            }
            else if (choice == "2")
            {
                // LogicForVisit will open StartingCity.
                return "StartingCity";
            }
            else if (choice == "9")
            {
                // Wait until Inventory is completely finished.
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