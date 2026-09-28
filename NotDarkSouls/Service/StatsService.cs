using LinqToDB;
using LinqToDB.Async;
using LinqToDB.Data;
using NotDarkSouls.DataBase;
using NotDarkSouls.Interfaces;

namespace NotDarkSouls.Service;

public class StatsService : IStats
{
    private readonly DataConnection _db;

    public StatsService(DataConnection db)
    {
        _db = db;
    }

    // Gets the stats row belonging to a player
    public async Task<StatsDB?> GetByPlayerIdAsync(
        int playerId,
        CancellationToken ct = default)
    {
        return await _db
            .GetTable<StatsDB>()
            .FirstOrDefaultAsync(
                x => x.PlayerId == playerId,
                ct);
    }

    // Creates a stats row for a player
    public async Task<StatsDB> CreateAsync(
        int playerId,
        StatsDB stats,
        CancellationToken ct = default)
    {
        stats.PlayerId = playerId;

        stats.StatsId =
            await _db.InsertWithInt32IdentityAsync(stats, token: ct);

        return stats;
    }

    // Updates a player's stats
    public async Task<bool> UpdateAsync(
        int playerId,
        StatsDB stats,
        CancellationToken ct = default)
    {
        var rows = await _db
            .GetTable<StatsDB>()
            .Where(x => x.PlayerId == playerId)
            .Set(x => x.Health, stats.Health)
            .Set(x => x.Strength, stats.Strength)
            .Set(x => x.Mana, stats.Mana)
            .Set(x => x.Dexterity, stats.Dexterity)
            .UpdateAsync(ct);

        return rows > 0;
    }

    // Deletes a player's stats row
    public async Task<bool> DeleteAsync(
        int playerId,
        CancellationToken ct = default)
    {
        var deleted = await _db
            .GetTable<StatsDB>()
            .Where(x => x.PlayerId == playerId)
            .DeleteAsync(ct);

        return deleted > 0;
    }
}