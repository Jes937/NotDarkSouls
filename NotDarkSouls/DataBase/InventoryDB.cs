using LinqToDB.Mapping;

namespace NotDarkSouls.DataBase;

[Table("InventoryDB")]
public class InventoryDB
{
    [PrimaryKey, Identity]
    public int InventoryId { get; set; }

    [Column("PlayerId"), NotNull]
    public int PlayerId { get; set; }

    [Column("Currency"), NotNull]
    public int Currency { get; set; }

    // one inventory, many items
    [Association(ThisKey = nameof(InventoryId), OtherKey = nameof(InventoryItemDB.InventoryId))]
    public List<InventoryItemDB> Items { get; set; } = new();
}