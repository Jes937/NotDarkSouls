using NotDarkSouls.Classes;

namespace NotDarkSouls.NPC;

public class BaseFriendlyNPC
{
    
    
    public InventoryLogic InventoryLogic { get; set; } = null!;
    
    
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<string> Responses { get; set; } = new();

    public List<String> Shop { get; set; } = new();


    public virtual Task TalkAsync(InventoryLogic playerInventory, int playerLvl)
    {
        Console.WriteLine($"{Name}: {Description}");

        foreach (var line in Responses)
            Console.WriteLine($"{Name}: {line}");

        return Task.CompletedTask;
    }
    
    public int InventoryOwnerId { get; protected set; }
    
    public virtual Task StockShopAsync() => Task.CompletedTask;
}