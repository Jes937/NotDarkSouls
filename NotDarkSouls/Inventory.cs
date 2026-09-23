using NotDarkSouls.Items;

namespace NotDarkSouls.Classes;

public class Inventory
{
    private readonly List<BaseItem> items = new();
    public BaseItem? EquippedWeapon { get; private set; }

    public IReadOnlyList<BaseItem> Items => items;

    public void AddItem(BaseItem item) => items.Add(item);

    public bool RemoveItem(string itemName)
    {
        var item = items.FirstOrDefault(i => i.Name == itemName);
        if (item == null) return false;
        items.Remove(item);
        return true;
    }

    public bool EquipItem(string itemName)
    {
        var item = items.FirstOrDefault(i => i.Name == itemName);
        if (item == null)
        {
            Console.WriteLine("Item not found in inventory.");
            return false;
        }

        EquippedWeapon = item;
        Console.WriteLine($"Equipped {item.Name}.");
        return true;
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n=== Inventory ===");
        if (!items.Any())
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        foreach (var item in items)
        {
            string equippedTag = item == EquippedWeapon ? " [EQUIPPED]" : "";
            Console.WriteLine($"{item.Name} | Damage: {item.Damage} | Type: {item.DamageType} | Weight: {item.Weight}{equippedTag}");
        }
    }
}