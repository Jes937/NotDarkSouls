namespace NotDarkSouls.Classes;

public static class ClassFactory
{
    private static readonly Dictionary<string, Func<BaseClass>> Classes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Warrior"] = () => new Warrior(),
            // ["Mage"] = () => new Mage(),
        };

    public static IReadOnlyList<string> Names => Classes.Keys.ToList();

    public static BaseClass? Create(string className) =>
        Classes.TryGetValue(className, out var create) ? create() : null;
}