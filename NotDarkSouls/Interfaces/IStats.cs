using NotDarkSouls.DataBase;

namespace NotDarkSouls.Interfaces;

public interface IStats
{
    Task<StatsDB?> GetByPlayerIdAsync(int playerId, CancellationToken ct = default);
    Task<StatsDB> CreateAsync(int playerId, StatsDB stats, CancellationToken ct = default);
    Task<bool> UpdateAsync(int playerId, StatsDB stats, CancellationToken ct = default);
    Task<bool> DeleteAsync(int playerId, CancellationToken ct = default);
}
