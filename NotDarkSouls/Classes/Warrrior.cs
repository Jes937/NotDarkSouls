using NotDarkSouls.Items;

namespace NotDarkSouls.Classes;

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

        InventoryLogic.AddItem(new WarriorStartingSword());
        InventoryLogic.AddItem(new ElfDagger());
        InventoryLogic.AddItem(new StandardGloves());
        InventoryLogic.AddItem(new ElfGloves());
        InventoryLogic.AddItem(new StandardChestplate());
    }

    public override void ShowStats()
    {
        Console.WriteLine("\n=== Warrior ===");
        base.ShowStats();
        
        Console.WriteLine($"Armor: {InventoryLogic.GetTotalArmor()}");
        Console.WriteLine($"Damage: {InventoryLogic.GetTotalDamage()}");
    }
}