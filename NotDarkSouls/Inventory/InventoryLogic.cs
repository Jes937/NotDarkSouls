using NotDarkSouls.Items;

namespace NotDarkSouls.Classes;

public class InventoryLogic
{
    public int currentCurrency = 25;

    public int RemoveCurrency(int amount)
    {
        currentCurrency -= amount;
        return currentCurrency;
    }

    public int Addcurrency(int amount)
    {
        currentCurrency += amount;
        return currentCurrency;
    }

    private readonly List<BaseItem> items = new();
    private readonly Dictionary<string, BaseItem> equippedItems = new();

    public IReadOnlyList<BaseItem> Items => items;
    public IReadOnlyDictionary<string, BaseItem> EquippedItems => equippedItems;

    //Does so you can add an item to the inventory 
    public void AddItem(BaseItem item) => items.Add(item);

    //Does so you can Remove items from your inventory
    public bool RemoveItem(string itemName)
    {
        var item = items.FirstOrDefault(i => i.Name == itemName);
        if (item == null) return false;
        items.Remove(item);
        return true;
    }

    //Checks whether this exact item is the one currently equipped in its type's slot
    public bool IsEquipped(BaseItem item) =>
        equippedItems.TryGetValue(item.ItemType, out var equipped) && equipped == item;
    
    //Does so the player can equip an item - only checks against other items of the same type
    public bool EquipItem(string itemName, int playerLvl)
    {
        var item = items.FirstOrDefault(i => i.Name == itemName);
        if (item == null)
        {
            Console.WriteLine("Item not found in inventory.");
            return false;
        }

        if (playerLvl < item.LvlReq)
        {
            Console.WriteLine($"You need to be level {item.LvlReq} to equip {item.Name}.");
            return false;
        }

        if (IsEquipped(item))
        {
            Console.WriteLine("Item is already equipped.");
            return false;
        }

        equippedItems[item.ItemType] = item;
        Console.WriteLine($"Equipped {item.Name}.");
        return true;
    }

    //Shows the Items in the players inventory
    public void ShowInventory()
    {
        Console.WriteLine("\n=== Inventory ===");
        Console.WriteLine($"Currency : {currentCurrency}");

        if (!items.Any())
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        foreach (var item in items)
        {
            string equippedTag = IsEquipped(item) ? " [EQUIPPED]" : "";
            Console.WriteLine($"{item.Name} | Damage: {item.Damage} | Type: {item.DamageType} | Weight: {item.Weight}{equippedTag}");
        }
    }
    
    //calculates the amount of armor you have on with all the equipped gear
    public int GetTotalArmor()
    {
        int totalArmor = 0;
        foreach (var item in EquippedItems.Values)
        {
            totalArmor += item.Armor;
        }
        return totalArmor;
    }
    
    //Calculates the amount of damage you do with the equipped weapons
    public int GetTotalDamage()
    {
        int totalDamage = 0;
        foreach (var item in EquippedItems.Values)
        {
            totalDamage += item.Damage;
        }
        return totalDamage;
    }
    
}