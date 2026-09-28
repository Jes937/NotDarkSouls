using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Data;
using NotDarkSouls.DataBase;
using NotDarkSouls.Interfaces;

namespace NotDarkSouls.Service;

public class InventoryService : IInventory
{
    private readonly DataConnection _db;

    public IInventoryItem Items { get; }

    public InventoryService(DataConnection db, IInventoryItem items)
    {
        _db = db;
        Items = items;
    }

    public async Task<InventoryDB?> GetByPlayerIdAsync(int playerId, CancellationToken ct = default)
    {
        var inventory = await _db.GetTable<InventoryDB>()
            .FirstOrDefaultAsync(x => x.PlayerId == playerId, ct);

        if (inventory == null) return null;

        inventory.Items = await _db.GetTable<InventoryItemDB>()
            .Where(x => x.InventoryId == inventory.InventoryId)
            .ToListAsync(ct);

        return inventory;
    }

    public async Task<InventoryDB> CreateAsync(int playerId, CancellationToken ct = default)
    {
        var inventory = new InventoryDB { PlayerId = playerId, Currency = 0 };
        inventory.InventoryId = await _db.InsertWithInt32IdentityAsync(inventory, token: ct);
        return inventory;
    }

    public async Task<bool> SetCurrencyAsync(int playerId, int currency, CancellationToken ct = default) =>
        await _db.GetTable<InventoryDB>()
            .Where(x => x.PlayerId == playerId)
            .Set(x => x.Currency, currency)
            .UpdateAsync(ct) > 0;

    public async Task<bool> DeleteAsync(int playerId, CancellationToken ct = default)
    {
        var inventory = await _db.GetTable<InventoryDB>()
            .FirstOrDefaultAsync(x => x.PlayerId == playerId, ct);

        if (inventory == null) return false;

        await _db.GetTable<InventoryItemDB>()
            .Where(x => x.InventoryId == inventory.InventoryId)
            .DeleteAsync(ct);

        return await _db.GetTable<InventoryDB>()
            .Where(x => x.InventoryId == inventory.InventoryId)
            .DeleteAsync(ct) > 0;
    }
}