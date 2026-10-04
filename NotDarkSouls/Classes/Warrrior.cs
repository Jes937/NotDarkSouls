using NotDarkSouls.Items;
using NotDarkSouls.Items.ItemsForPlayer.Boots;

namespace NotDarkSouls.Classes;

public class Warrior : BaseClass
{
    // Saved in PlayerDB.Class and used by ClassFactory
    public override string ClassName => "Warrior";

    public Warrior()
    {
        Lvl = 5;
        Health = 2;
        Strength = 3;
        Mana = 1;
        Dexterity = 1;
    }

    public override async Task AddStartingItemsAsync()
    {
        await InventoryLogic.AddItemAsync(new WarriorStartingSword());
        await InventoryLogic.AddItemAsync(new ElfDagger());
        await InventoryLogic.AddItemAsync(new StandardChestplate());
        await InventoryLogic.AddItemAsync(new WilliamsBoots());
    }
}