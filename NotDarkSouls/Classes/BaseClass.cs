namespace NotDarkSouls.Classes;

public abstract class BaseClass
{
    public int Lvl { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Mana { get; set; }
    public int Dexterity { get; set; }
    public int Experience { get; set; }
    public int Currency { get; set; }
    public Inventory Inventory { get; } = new();

    public void AddExperience(int amount)
    {
        Experience += amount;
        Console.WriteLine($"Gained {amount} XP ({Experience}/{ExperienceToNextLevel()})");

        while (Experience >= ExperienceToNextLevel())
        {
            Experience -= ExperienceToNextLevel();
            LevelUp();
        }
    }

    public virtual void ShowStats()
    {
        Console.WriteLine($"\nLevel: {Lvl}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Strength: {Strength}");
        Console.WriteLine($"Mana: {Mana}");
        Console.WriteLine($"Dexterity: {Dexterity}");
        Console.WriteLine($"Experience: {Experience}/{ExperienceToNextLevel()}");
        Console.WriteLine($"Currency: {Currency}");
    }

    protected virtual int ExperienceToNextLevel()
    {
        return Lvl * 100;
    }

    protected virtual void LevelUp()
    {
        Lvl++;
        int levelPoints = 3;

        Console.WriteLine($"Level up! You are now level {Lvl}.");

        while (levelPoints > 0)
        {
            Console.WriteLine($"\nYou have {levelPoints} point(s) to spend.");
            Console.WriteLine("Health [1]");
            Console.WriteLine("Strength [2]");
            Console.WriteLine("Mana [3]");
            Console.WriteLine("Dexterity [4]");

            switch (Console.ReadLine())
            {
                case "1":
                    Health++;
                    levelPoints--;
                    Console.WriteLine("Health increased!");
                    break;
                case "2":
                    Strength++;
                    levelPoints--;
                    Console.WriteLine("Strength increased!");
                    break;
                case "3":
                    Mana++;
                    levelPoints--;
                    Console.WriteLine("Mana increased!");
                    break;
                case "4":
                    Dexterity++;
                    levelPoints--;
                    Console.WriteLine("Dexterity increased!");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}