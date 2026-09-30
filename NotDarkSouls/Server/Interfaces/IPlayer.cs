using NotDarkSouls.DataBase;

namespace NotDarkSouls.Interfaces;

public interface IPlayer
{
    Task<PlayerDB?> GetByIdAsync(int playerId, CancellationToken ct = default);
    Task<IReadOnlyList<PlayerDB>> GetAllAsync(CancellationToken ct = default);
    Task<PlayerDB> CreateAsync(PlayerDB player, CancellationToken ct = default);
    Task<bool> UpdateAsync(int playerId, PlayerDB player, CancellationToken ct = default);
    Task<bool> DeleteAsync(int playerId, CancellationToken ct = default);

    // Children
    IStats Stats { get; }
    IInventory Inventory { get; }
}