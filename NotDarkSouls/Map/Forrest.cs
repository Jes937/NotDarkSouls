using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public class Forrest
{
    public static string Visit(BaseClass player)
    {
        Console.WriteLine("Welcome you are now standing in the forrest of the forbiden");

        while (true)
        {
            Console.WriteLine("\nExplore the forrest [1], Go to the nearest city [2], Inventory [9], Menu[0]");
            var choice = Console.ReadLine();

            if (choice == "1")
            {

            }
            else if (choice == "2")
            {
                //skal lige rette det her så den ikke stacer loops 
                StartingCity.Visit(player);

            }
            else if (choice == "3")
            {

            }
            else if (choice == "4")
            {

            }
            else if (choice == "9")
            {
                InventoryMenu.ShowInventoryMenu(player);
            }
            else if (choice == "0")
            {
                Console.WriteLine("Returning to menu...");
                return "Menu";
            }
            else
            {
                Console.WriteLine("pls make a valid choice");
            }
        }
    }
}