namespace NotDarkSouls.Classes;

public class BaseClass
{
    
    // PLAYER
    
    public int PlayerId { get; set; }


    
    // PLAYER STATS
    
    public int Lvl { get; set; }

    public int Health { get; set; }

    public int Strength { get; set; }

    public int Mana { get; set; }

    public int Dexterity { get; set; }

    public int Experience { get; set; }
    
    public int Armor { get; set; }


    
    // INVENTORY
    

    // Set in Program.cs right after the character is created or loaded
    public InventoryLogic InventoryLogic { get; set; } = null!;


    
    // EXPERIENCE
    public void AddExperience(int amount)
    {
        Experience += amount;

        Console.WriteLine(
            $"Gained {amount} XP ({Experience}/{ExperienceToNextLevel()})"
        );

        while (Experience >= ExperienceToNextLevel())
        {
            Experience -= ExperienceToNextLevel();

            Lvl++;

            Console.WriteLine(
                $"Level up! You are now level {Lvl}."
            );
        }
    }
    
    // EXPERIENCE NEEDED
    
    public int ExperienceToNextLevel()
    {
        return Lvl * 100;
    }
    
    // SHOW STATS
    

    public virtual void ShowStats()
    {
        Console.WriteLine();

        Console.WriteLine($"Level: {Lvl}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Strength: {Strength}");
        Console.WriteLine($"Mana: {Mana}");
        Console.WriteLine($"Dexterity: {Dexterity}");
        
        int equippedArmor = InventoryLogic.GetTotalArmor();
        Console.WriteLine($"Armor: {Armor + equippedArmor} ({equippedArmor} equipped)");

        Console.WriteLine(
            $"Experience: {Experience}/{ExperienceToNextLevel()}"
        );
    }
    
    
// Saved in PlayerDB.Class, and used to find the class again on load
    public virtual string ClassName => GetType().Name;

// Each class overrides this to give its starting items
    public virtual Task AddStartingItemsAsync() => Task.CompletedTask;
}