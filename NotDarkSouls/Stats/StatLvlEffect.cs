namespace NotDarkSouls.Classes;

public class StatLevelEffect
{
    public int StrengthBonus { get; set; }
    public int HealthBonus { get; set; }
    public int ManaBonus { get; set; }
    public int DexterityBonus { get; set; }
    public int CurrencyBonus { get; set; }
    public string Description { get; set; } = "";

    public void ApplyTo(BaseClass character)
    {
        character.Strength += StrengthBonus;
        character.Health += HealthBonus;
        character.Mana += ManaBonus;
        character.Dexterity += DexterityBonus;
        character.Currency += CurrencyBonus;
    }
}