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
                "Show stats [3], " +
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
