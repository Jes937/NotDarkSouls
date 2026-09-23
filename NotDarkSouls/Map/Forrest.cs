using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public class Forrest
{
    public void startForrest(BaseClass player)
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

            }
            else if (choice == "3")
            {

            }
            else if (choice == "4")
            {

            }
            else if (choice == "9")
            {
                //Need to make a seperate class for this
                bool inInventoryMenu = true;
                while (inInventoryMenu)
                {
                    Console.WriteLine("\n=== Inventory Menu ===");
                    Console.WriteLine("Show inventory [1], Equip item [2], Show stats [3], Back [0]");
                    var invChoice = Console.ReadLine();

                    if (invChoice == "1")
                    {
                        player.Inventory.ShowInventory();
                    }
                    else if (invChoice == "2")
                    {
                        player.Inventory.ShowInventory();
                        Console.WriteLine("Enter the name of the item to equip:");
                        string? itemName = Console.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(itemName))
                        {
                            player.Inventory.EquipItem(itemName);
                        }
                    }
                    else if (invChoice == "3")
                    {
                        player.ShowStats();
                    }
                    else if (invChoice == "0")
                    {
                        inInventoryMenu = false;
                    }
                    else
                    {
                        Console.WriteLine("pls make a valid choice");
                    }
                }
            }
            else if (choice == "0")
            {
                Console.WriteLine("Returning to menu...");
                break;
            }
            else
            {
                Console.WriteLine("pls make a valid choice");
            }
        }
    }
}