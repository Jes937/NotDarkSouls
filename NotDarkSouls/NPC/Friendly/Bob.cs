using NotDarkSouls.Items;
using NotDarkSouls.Items.ItemsForPlayer.Boots;

namespace NotDarkSouls.NPC.Friendly;

public class Bob : BaseFriendlyNPC
{
    public Bob()
    {
        Name = "Bob";
        Description = "Bob the merchant was a wandering trader before he got ambushed by the goblins.";
        Responses.Add("Hello there");
        
    }

    public void Talk()
    {
        // Greeting from the base class's Responses list
        foreach (var line in Responses)
        {
            Console.WriteLine($"{Name}: {line}");
        }

        while (true)
        {
            Console.WriteLine("Description [1]");
            Console.WriteLine("Quest [2]");
            Console.WriteLine("Shop [3]");
            Console.WriteLine("Leave [0]");

            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                Console.WriteLine(Description);
            }
            else if (choice == "2")
            {
                Console.WriteLine(BobsQuest);
            }
            else if (choice == "3")
            {
                ShowShop();
            }
            
            else if (choice == "4")
            {
                
                
            }
            else if (choice == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }


    private void ShowShop()
    {
        var items = InventoryLogic.Items; // guess: use whatever InventoryLogic exposes

        if (items.Count == 0)
        {
            Console.WriteLine($"{Name}: I have nothing to sell right now.");
            return;
        }

        Console.WriteLine($"{Name}'s shop:");

        for (int i = 0; i < items.Count; i++)
            Console.WriteLine($"[{i + 1}] {items[i].Name}"); // guess: property holding the item name
    }
    public string BobsQuest => "Bobs Quest";
}