using NotDarkSouls.Classes;
using NotDarkSouls.Items;

public class Warrior : BaseClass
{
    public int Armor { get; set; }

    public Warrior()
    {
        Lvl = 1;
        Health = 2;
        Strength = 3;
        Mana = 1;
        Dexterity = 1;
    }

    public async Task AddStartingItemsAsync()
    {
        await InventoryLogic.AddItemAsync(new WarriorStartingSword());
        await InventoryLogic.AddItemAsync(new ElfDagger());
        await InventoryLogic.AddItemAsync(new OrcArm());
        await InventoryLogic.AddItemAsync(new StandardChestplate());
    }
}