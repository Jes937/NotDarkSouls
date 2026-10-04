namespace NotDarkSouls.NPC;

public class BaseFriendlyNPC
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<string> Responses { get; set; } = new();

    public virtual void Talk()
    {
        Console.WriteLine($"{Name}: {Description}");

        foreach (var line in Responses)
        {
            Console.WriteLine($"{Name}: {line}");
        }
    }
}