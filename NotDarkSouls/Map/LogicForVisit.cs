using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public static class LogicForVisit
{
    public static async Task Run(BaseClass player)
    {
        string currentLocation = "Forrest";

        while (currentLocation != "Menu")
        {
            currentLocation = currentLocation switch
            {
                "Forrest" =>
                    await Forrest.Visit(player),

                "StartingCity" =>
                    await StartingCity.Visit(player),

                _ => "Menu"
            };
        }
    }
}