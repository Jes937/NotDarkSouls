using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Data;
using NotDarkSouls.DataBase;
using NotDarkSouls.Interfaces;

namespace NotDarkSouls.Service;

public class InventoryItemService : IInventoryItem
{
    private readonly DataConnection _db;

    public InventoryItemService(DataConnection db)
    {
        _db = db;
    }

    // Gets one item from a specific inventory
    public async Task<InventoryItemDB?> GetByIdAsync(
        int inventoryId,
        int itemId,
        CancellationToken ct = default)
    {
        return await _db
            .GetTable<InventoryItemDB>()
            .FirstOrDefaultAsync(
                x => x.InventoryId == inventoryId &&
                     x.InventoryItemId == itemId,
                ct);
    }

    // Gets all items from a specific inventory
    public async Task<IReadOnlyList<InventoryItemDB>> GetAllAsync(
        int inventoryId,
        CancellationToken ct = default)
    {
        return await _db
            .GetTable<InventoryItemDB>()
            .Where(x => x.InventoryId == inventoryId)
            .ToListAsync(ct);
    }

    // Adds an item to an inventory
    public async Task<InventoryItemDB> AddAsync(
        int inventoryId,
        InventoryItemDB item,
        CancellationToken ct = default)
    {
        item.InventoryId = inventoryId;

        item.InventoryItemId =
            await _db.InsertWithInt32IdentityAsync(item);

        return item;
    }

    // Removes an item from an inventory
    public async Task<bool> RemoveAsync(
        int inventoryId,
        int itemId,
        CancellationToken ct = default)
    {
        var deleted = await _db
            .GetTable<InventoryItemDB>()
            .Where(x =>
                x.InventoryId == inventoryId &&
                x.InventoryItemId == itemId)
            .DeleteAsync();

        return deleted > 0;
    }

    // Equips an item
    // Only one item of the same ItemType can be equipped
    public async Task<bool> EquipAsync(
        int inventoryId,
        int itemId,
        CancellationToken ct = default)
    {
        var item = await GetByIdAsync(inventoryId, itemId, ct);

        if (item == null)
            return false;

        // Unequip other items of the same type
        await _db
            .GetTable<InventoryItemDB>()
            .Where(x =>
                x.InventoryId == inventoryId &&
                x.ItemType == item.ItemType &&
                x.InventoryItemId != itemId)
            .Set(x => x.IsEquipped, false)
            .UpdateAsync();

        // Equip the selected item
        item.IsEquipped = true;

        return await _db
                   .UpdateAsync(item) > 0;
    }

    // Unequips an item
    public async Task<bool> UnequipAsync(
        int inventoryId,
        int itemId,
        CancellationToken ct = default)
    {
        var item = await GetByIdAsync(inventoryId, itemId, ct);

        if (item == null)
            return false;

        item.IsEquipped = false;

        return await _db
                   .UpdateAsync(item) > 0;
    }
}