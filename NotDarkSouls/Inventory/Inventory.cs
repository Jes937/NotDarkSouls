namespace NotDarkSouls.Classes;

public static class InventoryMenu
{
    public static void ShowInventoryMenu(BaseClass player)
    {
        bool inInventoryMenu = true;
        while (inInventoryMenu)
        {
            Console.WriteLine("\n=== Inventory Menu ===");
            Console.WriteLine("Show inventory [1], Equip item [2], Show stats [3], Back [0]");
            var invChoice = Console.ReadLine();

            if (invChoice == "1")
            {
                player.InventoryLogic.ShowInventory();
            }
            else if (invChoice == "2")
            {
                player.InventoryLogic.ShowInventory();
                Console.WriteLine("Enter the name of the item to equip:");
                string? itemName = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(itemName))
                {
                    player.InventoryLogic.EquipItem(itemName, player.Lvl);
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
}