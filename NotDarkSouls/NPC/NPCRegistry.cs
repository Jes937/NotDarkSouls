using NotDarkSouls.NPC.Friendly;

namespace NotDarkSouls.NPC;

public static class NpcRegistry
{
    private static readonly Dictionary<Type, BaseFriendlyNPC> Npcs = new();

    public static void Register(BaseFriendlyNPC npc) => Npcs[npc.GetType()] = npc;

    public static T Get<T>() where T : BaseFriendlyNPC => (T)Npcs[typeof(T)];
}