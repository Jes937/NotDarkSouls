using LinqToDB.Mapping;

namespace NotDarkSouls.DataBase;

[Table("InventoryItemDB")]
public class InventoryItemDB
{
    [PrimaryKey, Identity]
    public int InventoryItemId { get; set; }

    [Column("InventoryId"), NotNull]
    public int InventoryId { get; set; }

    [Column("ItemName"), NotNull]
    public string ItemName { get; set; } = "";

    [Column("ItemType"), NotNull]
    public string ItemType { get; set; } = "";

    [Column("IsEquipped"), NotNull]
    public bool IsEquipped { get; set; }
}