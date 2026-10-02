using NotDarkSouls.Classes;

namespace NotDarkSouls.Classes.Map;

public static class LogicForVisit
{
    public static async Task Run(
        BaseClass player,
        string startLocation,
        Func<string, Task>? onLocationChanged = null)
    {
        string currentLocation = startLocation;

        while (currentLocation != "Menu")
        {
            // Save where the player is before they do anything there
            if (onLocationChanged != null)
                await onLocationChanged(currentLocation);

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