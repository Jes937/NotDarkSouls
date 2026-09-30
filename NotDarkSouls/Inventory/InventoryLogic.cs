using NotDarkSouls.DataBase;
using NotDarkSouls.Interfaces;
using NotDarkSouls.Items;

namespace NotDarkSouls.Classes;

// The player's inventory while playing.
// The database is the source of truth: every change is written to the database first,
// and the in-memory list is only updated when that write succeeded.
public class InventoryLogic
{
    // One row in InventoryItemDB together with the game item it stands for
    private sealed class Entry
    {
        public Entry(int inventoryItemId, BaseItem item, bool isEquipped)
        {
            InventoryItemId = inventoryItemId;
            Item = item;
            IsEquipped = isEquipped;
        }

        public int InventoryItemId { get; }   // primary key of the database row
        public BaseItem Item { get; }
        public bool IsEquipped { get; set; }
    }

    private readonly IInventory _inventoryService;
    private readonly int _playerId;
    private readonly int _inventoryId;
    private readonly List<Entry> _entries = new();

    public int CurrentCurrency { get; private set; }

    private InventoryLogic(IInventory inventoryService, int playerId, int inventoryId, int currency)
    {
        _inventoryService = inventoryService;
        _playerId = playerId;
        _inventoryId = inventoryId;
        CurrentCurrency = currency;
    }

    // ---------- Create / load ----------

    // New character: creates the inventory row in the database
    public static async Task<InventoryLogic> CreateAsync(
        IInventory inventoryService,
        int playerId,
        int startingCurrency = 25)
    {
        var row = await inventoryService.CreateAsync(playerId);
        await inventoryService.SetCurrencyAsync(playerId, startingCurrency);

        return new InventoryLogic(inventoryService, playerId, row.InventoryId, startingCurrency);
    }

    // Existing character: loads currency, items and equipped state from the database
    public static async Task<InventoryLogic?> LoadAsync(IInventory inventoryService, int playerId)
    {
        var row = await inventoryService.GetByPlayerIdAsync(playerId);
        if (row == null) return null;

        var inventory = new InventoryLogic(inventoryService, playerId, row.InventoryId, row.Currency);

        foreach (var itemRow in row.Items)
        {
            var item = ItemCatalog.Create(itemRow.ItemName);
            if (item == null)
            {
                Console.WriteLine($"Unknown item '{itemRow.ItemName}' in the database - skipped.");
                continue;
            }

            inventory._entries.Add(new Entry(itemRow.InventoryItemId, item, itemRow.IsEquipped));
        }

        return inventory;
    }

    // ---------- Items ----------

    public IReadOnlyList<BaseItem> Items => _entries.Select(e => e.Item).ToList();

    public IReadOnlyDictionary<string, BaseItem> EquippedItems =>
        _entries.Where(e => e.IsEquipped).ToDictionary(e => e.Item.ItemType, e => e.Item);

    // Adds an item to the inventory and saves it
    public async Task AddItemAsync(BaseItem item)
    {
        var row = await _inventoryService.Items.AddAsync(_inventoryId, new InventoryItemDB
        {
            ItemName = item.Name,
            ItemType = item.ItemType,
            IsEquipped = false
        });

        _entries.Add(new Entry(row.InventoryItemId, item, false));
    }

    // Removes an item from the inventory and the database
    public async Task<bool> RemoveItemAsync(string itemName)
    {
        var entry = FindByName(itemName);
        if (entry == null) return false;

        if (!await _inventoryService.Items.RemoveAsync(_inventoryId, entry.InventoryItemId))
            return false;

        _entries.Remove(entry);
        return true;
    }

    // Checks whether this exact item is currently equipped
    public bool IsEquipped(BaseItem item) =>
        _entries.Any(e => e.IsEquipped && e.Item == item);

    // Equips an item (only one item per ItemType can be equipped) and saves it
    public async Task<bool> EquipItemAsync(string itemName, int playerLvl)
    {
        var entry = FindByName(itemName);
        if (entry == null)
        {
            Console.WriteLine("Item not found in inventory.");
            return false;
        }

        if (playerLvl < entry.Item.LvlReq)
        {
            Console.WriteLine($"You need to be level {entry.Item.LvlReq} to equip {entry.Item.Name}.");
            return false;
        }

        if (entry.IsEquipped)
        {
            Console.WriteLine("Item is already equipped.");
            return false;
        }

        // The database service unequips the other items of the same type for us
        if (!await _inventoryService.Items.EquipAsync(_inventoryId, entry.InventoryItemId))
            return false;

        // Keep the in-memory copy in sync with the database
        foreach (var other in _entries.Where(e => e.Item.ItemType == entry.Item.ItemType))
            other.IsEquipped = false;

        entry.IsEquipped = true;
        Console.WriteLine($"Equipped {entry.Item.Name}.");
        return true;
    }

    private Entry? FindByName(string itemName) =>
        _entries.FirstOrDefault(e => e.Item.Name == itemName);

    // ---------- Currency ----------

    public async Task<int> AddCurrencyAsync(int amount)
    {
        var newTotal = CurrentCurrency + amount;

        if (await _inventoryService.SetCurrencyAsync(_playerId, newTotal))
            CurrentCurrency = newTotal;

        return CurrentCurrency;
    }

    public Task<int> RemoveCurrencyAsync(int amount) => AddCurrencyAsync(-amount);

    // ---------- Display and totals ----------

    // Shows the items in the player's inventory
    public void ShowInventory()
    {
        Console.WriteLine("\n=== Inventory ===");
        Console.WriteLine($"Currency : {CurrentCurrency}");

        if (_entries.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        foreach (var entry in _entries)
        {
            var item = entry.Item;
            string equippedTag = entry.IsEquipped ? " [EQUIPPED]" : "";
            Console.WriteLine($"{item.Name} | Damage: {item.Damage} | Type: {item.DamageType} | Weight: {item.Weight}{equippedTag}");
        }
    }

    // Total armor of everything that is equipped
    public int GetTotalArmor() => _entries.Where(e => e.IsEquipped).Sum(e => e.Item.Armor);

    // Total damage of everything that is equipped
    public int GetTotalDamage() => _entries.Where(e => e.IsEquipped).Sum(e => e.Item.Damage);
}