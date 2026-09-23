using NotDarkSouls.Classes;
using NotDarkSouls.Items;
using NotDarkSouls.Classes.Map;

Console.WriteLine("What is your name?");
String playerName = Console.ReadLine()?.Trim();
Console.WriteLine("Welcome "  + playerName);

Forrest forrest = new  Forrest();

while (true)
{
    
    
    Console.WriteLine("Chose a class: Warrior [1]");
    
    var choice = Console.ReadLine();
    
    
    if (choice == "1")
        
    {
        
        var player = new Warrior();
        player.ShowStats();
        Console.WriteLine("Do you want to continue? y/n");
        string answer = Console.ReadLine()?.Trim().ToLower();
        
        
        if (answer == "y")
        {
            forrest.startForrest(player);
        }
        
        if (answer == "n")
        {
            player.AddExperience(500);
            continue; // går tilbage til class menuen
        }
        
    }
    
}