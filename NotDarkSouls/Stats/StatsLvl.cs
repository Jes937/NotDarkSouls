namespace NotDarkSouls.Classes;

public class StatsLvl
{
    private readonly Dictionary<int, StatLevelEffect> _strengthLevels = new()
    {
        { 1, new StatLevelEffect { StrengthBonus = 1, Description = "+1 Strength" } },
        { 2, new StatLevelEffect { StrengthBonus = 1, HealthBonus = 1, Description = "+1 Strength"} },
        { 3, new StatLevelEffect { StrengthBonus = 1, CurrencyBonus = 10, Description = "+1 Strength" } },
    };

    private readonly Dictionary<int, StatLevelEffect> _dexterityLevels = new()
    {
        { 1, new StatLevelEffect { DexterityBonus = 1, Description = "+1 Dexterity" } },
        { 2, new StatLevelEffect { DexterityBonus = 2, Description = "+2 Dexterity" } },
    };

    private readonly Dictionary<int, StatLevelEffect> _manaLevels = new()
    {
        { 1, new StatLevelEffect { ManaBonus = 1, Description = "+1 Mana" } },
        { 2, new StatLevelEffect { ManaBonus = 3, Description = "+3 Mana" } },
    };

    // apply AND tell the caller what happened, so you can print it
    public string ApplyStrengthLevel(BaseClass character, int level)
        => Apply(_strengthLevels, character, level);

    public string ApplyDexterityLevel(BaseClass character, int level)
        => Apply(_dexterityLevels, character, level);

    public string ApplyManaLevel(BaseClass character, int level)
        => Apply(_manaLevels, character, level);

    private string Apply(Dictionary<int, StatLevelEffect> table, BaseClass character, int level)
    {
        if (!table.TryGetValue(level, out var effect))
            return $"No effect defined for level {level}.";

        effect.ApplyTo(character);
        return effect.Description;
    }

    // so the player can preview what a level gives WITHOUT applying it yet
    public string PreviewStrengthLevel(int level)
        => _strengthLevels.TryGetValue(level, out var effect) ? effect.Description : "No effect defined.";
}