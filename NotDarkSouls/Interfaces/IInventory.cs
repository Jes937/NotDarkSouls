using NotDarkSouls.DataBase;

namespace NotDarkSouls.Interfaces;

public interface IInventory
{
    Task<InventoryDB?> GetByPlayerIdAsync(int playerId, CancellationToken ct = default);
    Task<InventoryDB> CreateAsync(int playerId, CancellationToken ct = default);
    Task<bool> SetCurrencyAsync(int playerId, int currency, CancellationToken ct = default);
    Task<bool> DeleteAsync(int playerId, CancellationToken ct = default);

    IInventoryItem Items { get; }
}