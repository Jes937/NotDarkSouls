using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public static class LogicForVisit
{
    public static void Run(BaseClass player)
    {
        string currentLocation = "Forrest";
        while (currentLocation != "Menu")
        {
            currentLocation = currentLocation switch
            {
                "Forrest" => Forrest.Visit(player),
                "StartingCity" => StartingCity.Visit(player),
                _ => "Menu"
            };
        }
    }
}