namespace NotDarkSouls.NPC.Friendly;

public class Bob : BaseFriendlyNPC
{
    public  Bob()
    {
        Name = "Bob the merchant";

        Description = "Bob the merchant was a wandering trader before he got ambushed by the goblins.";

        Responses.Add("Hello there");
    }
}