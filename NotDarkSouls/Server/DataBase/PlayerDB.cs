using LinqToDB.Mapping;

namespace NotDarkSouls.DataBase;

[Table("PlayerDB")]
public class PlayerDB
{
    [PrimaryKey, Identity]
    public int PlayerId { get; set; }

    [Column("PlayerName"), NotNull]
    public string PlayerName { get; set; } = "";

    [Column("Class"), NotNull]
    public string Class { get; set; } = "";

    [Column("Level"), NotNull]
    public int Level { get; set; }
    
    [Column("Location")]
    [NotNull]
    public string Location { get; set; } = "Forrest";

    // one-to-one: one player, one stats row
    [Association(ThisKey = nameof(PlayerId), OtherKey = nameof(StatsDB.PlayerId), CanBeNull = true)]
    public StatsDB? Stats { get; set; }
    
    [Association(ThisKey = nameof(PlayerId), OtherKey = nameof(InventoryDB.PlayerId), CanBeNull = true)]
    public InventoryDB? Inventory { get; set; }
}