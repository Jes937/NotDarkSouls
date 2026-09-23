namespace NotDarkSouls.Items;
    
public class BaseItem

{
    
public string Name { get; set; } = string.Empty;

public int LvlReq { get; set; }

public string DamageType { get; set; } = string.Empty;
public int Damage { get; set; }
public int Weight { get; set; }

}