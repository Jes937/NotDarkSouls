using NotDarkSouls.Items.ItemsForPlayer.Boots;

namespace NotDarkSouls.Items;

// The database only stores an item's name, so this turns a saved name back into a real item object.
// Every item in the game has to be listed here, and every item needs a unique Name.
public static class ItemCatalog
{
    private static readonly Func<BaseItem>[] Factories =
    {
        () => new WarriorStartingSword(),
        
        //OneHandedSwords
        () => new ElfDagger(),
        
        //TwoHandedSwords
         
         
        //ChestPlate 
        () => new StandardChestplate(),
        
        //Gloves 
        () => new StandardGloves(),
        
        //Boots 
        
        () => new WilliamsBoots(),
        
        //Helmet
        
        
        
        
        
        
       
        
        // add new item classes here
    };

    private static readonly Dictionary<string, Func<BaseItem>> FactoriesByName =
        Factories.ToDictionary(factory => factory().Name);

    // Creates a fresh item object for a saved item name, or null if the name is unknown
    public static BaseItem? Create(string name) =>
        FactoriesByName.TryGetValue(name, out var factory) ? factory() : null;
}