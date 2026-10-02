namespace NotDarkSouls.Classes;

public static class Inventory
{
    public static async Task ShowInventory(BaseClass player)
    {
        bool inInventoryMenu = true;

        while (inInventoryMenu)
        {
            Console.WriteLine();
            Console.WriteLine("=== Inventory Menu ===");

            Console.WriteLine(
                "Show inventory [1], " +
                "Equip item [2], " +
                "Remove item [3], " +
                "Show stats [4], " +
                "Back [0]"
            );

            string? invChoice =
                Console.ReadLine()?.Trim();

            if (invChoice == "1")
            {
                player.InventoryLogic.ShowInventory();
            }

            else if (invChoice == "2")
            {
                player.InventoryLogic.ShowInventory();

                if (player.InventoryLogic.Items.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "You don't have any items to equip."
                    );

                    continue;
                }

                Console.WriteLine();

                Console.WriteLine(
                    "Enter the name of the item to equip:"
                );

                string? itemName =
                    Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(itemName))
                {
                    Console.WriteLine(
                        "Please enter a valid item name."
                    );

                    continue;
                }

                await player.InventoryLogic.EquipItemAsync(
                    itemName,
                    player.Lvl
                );
            }
            else if (invChoice == "3")
            {
                player.InventoryLogic.ShowInventory();

                if (player.InventoryLogic.Items.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have any items to drop.");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Enter the name of the item to drop:");
                
                string? itemName = Console.ReadLine()?.Trim();
                
                if (string.IsNullOrWhiteSpace(itemName))
                {
                    Console.WriteLine("Please enter a valid item name.");
                    
                    continue;
                }

                bool removed = await player.InventoryLogic.RemoveItemAsync(itemName);
                
                Console.WriteLine($"You sure you want to drop this item {itemName} Y/N");
                String UserInput  = Console.ReadLine().ToLower().Trim();
                if (UserInput == "y")
                {
                   Console.WriteLine(removed
                                       ? $"Dropped {itemName}."
                                       : $"You don't have an item called '{itemName}'."); 
                }

                if (UserInput == "n")
                {
                    Console.WriteLine("Did not drop item");
                }
                
            }
            

            else if (invChoice == "4")
            {
                player.ShowStats();
            }

            else if (invChoice == "0")
            {
                inInventoryMenu = false;
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
