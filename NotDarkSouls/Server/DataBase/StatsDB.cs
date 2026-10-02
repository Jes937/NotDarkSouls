using LinqToDB.Mapping;

namespace NotDarkSouls.DataBase;

[Table("StatsDB")]
public class StatsDB
{
    [PrimaryKey, Identity]
    public int StatsId { get; set; }

    [Column("PlayerId"), NotNull]   
    public int PlayerId { get; set; }

    [Column("Health"), NotNull]
    public int Health { get; set; }

    [Column("Strength"), NotNull]
    public int Strength { get; set; }

    [Column("Mana"), NotNull]
    public int Mana { get; set; }

    [Column("Dexterity"), NotNull]
    public int Dexterity { get; set; }
}