using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Data;
using NotDarkSouls.DataBase;
using NotDarkSouls.Interfaces;

namespace NotDarkSouls.Service;

public class PlayerService : IPlayer
{
    private readonly DataConnection _db;

    public IStats Stats { get; }
    public IInventory Inventory { get; }

    public PlayerService(DataConnection db, IStats stats, IInventory inventory)
    {
        _db = db;
        Stats = stats;
        Inventory = inventory;
    }

    public Task<PlayerDB?> GetByIdAsync(int playerId, CancellationToken ct = default) =>
        _db.GetTable<PlayerDB>().FirstOrDefaultAsync(x => x.PlayerId == playerId, ct);

    public async Task<IReadOnlyList<PlayerDB>> GetAllAsync(CancellationToken ct = default) =>
        await _db.GetTable<PlayerDB>().ToListAsync(ct);

    public async Task<PlayerDB> CreateAsync(PlayerDB player, CancellationToken ct = default)
    {
        player.PlayerId = await _db.InsertWithInt32IdentityAsync(player, token: ct);
        return player;
    }

    public async Task<bool> UpdateAsync(int playerId, PlayerDB player, CancellationToken ct = default)
    {
        player.PlayerId = playerId;
        return await _db.UpdateAsync(player, token: ct) > 0;
    }

    public async Task<bool> DeleteAsync(int playerId, CancellationToken ct = default)
    {
        await using var transaction = await _db.BeginTransactionAsync(ct);

        try
        {
            await Inventory.DeleteAsync(playerId, ct); // deletes inventory items, then the inventory row
            await Stats.DeleteAsync(playerId, ct);

            var deleted = await _db
                .GetTable<PlayerDB>()
                .Where(x => x.PlayerId == playerId)
                .DeleteAsync(ct);

            await transaction.CommitAsync(ct);
            return deleted > 0;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}