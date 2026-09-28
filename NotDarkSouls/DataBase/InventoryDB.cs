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

    // Not a linq2db association — filled manually in GetByPlayerIdAsync
    [NotColumn]
    public List<InventoryItemDB> Items { get; set; } = new();
}