using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public static class StartingCity
{
    public static void SmallTown(BaseClass player)
    {
        Console.WriteLine("Welcome stranger");
        while (true)
        {
            Console.WriteLine("Go back [8], Inventory [9], Menu [0]");
            var choice = Console.ReadLine();

            if (choice == "8")
            {
                return;
            }
            else if (choice == "9")
            {
                InventoryMenu.ShowInventoryMenu(player);
            }
            else
            {
                Console.WriteLine("pls make a valid choice");
            }
        }
    }
}