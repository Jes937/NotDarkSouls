using NotDarkSouls.DataBase;

namespace NotDarkSouls.Interfaces;

public interface IInventoryItem
{
    Task<InventoryItemDB?> GetByIdAsync(int inventoryId, int itemId, CancellationToken ct = default);
    Task<IReadOnlyList<InventoryItemDB>> GetAllAsync(int inventoryId, CancellationToken ct = default);
    Task<InventoryItemDB> AddAsync(int inventoryId, InventoryItemDB item, CancellationToken ct = default);
    Task<bool> RemoveAsync(int inventoryId, int itemId, CancellationToken ct = default);

    // Stored in the IsEquipped column; should enforce one equipped item per ItemType
    Task<bool> EquipAsync(int inventoryId, int itemId, CancellationToken ct = default);
    Task<bool> UnequipAsync(int inventoryId, int itemId, CancellationToken ct = default);
}