namespace NotDarkSouls.Items;
    
public class BaseItem

{
    
    /*
     * Item types
     * OneHandedWeapon
     * TwohandedWeapon
     * Helmet
     * ShoulderPats
     * ChestPlate
     * Gloves 
     * Boots
     * 
     */
public string ItemType{get;set;}    
public string Name { get; set; } = string.Empty;
public int LvlReq { get; set; }
public string DamageType { get; set; } = string.Empty;
public int Damage { get; set; }

public int Armor { get; set; }
public int Weight { get; set; }

}