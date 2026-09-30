using LinqToDB;
using LinqToDB.Data;
using NotDarkSouls.Classes;
using NotDarkSouls.Classes.Map;
using NotDarkSouls.DataBase;
using NotDarkSouls.Service;


// DATABASE CONNECTION (SQLite)

await using var db = new DataConnection(
    new DataOptions().UseSQLite("Data Source=notdarksouls.db"));

// linq2db does not create tables by itself
db.CreateTable<PlayerDB>(tableOptions: TableOptions.CheckExistence);
db.CreateTable<StatsDB>(tableOptions: TableOptions.CheckExistence);
db.CreateTable<InventoryDB>(tableOptions: TableOptions.CheckExistence);
db.CreateTable<InventoryItemDB>(tableOptions: TableOptions.CheckExistence);



// SERVICES
var playerService = new PlayerService(
    db,
    new StatsService(db),
    new InventoryService(db, new InventoryItemService(db)));



// PLAYER NAME

Console.WriteLine("What is your name?");

string playerName =
    Console.ReadLine()?.Trim() ?? "Player";

Console.WriteLine($"Welcome {playerName}");



// MAIN MENU
while (true)
{
    Console.WriteLine();

    Console.WriteLine("Choose a class: Warrior [1]");
    Console.WriteLine("Exit [0]");

    string? choice =
        Console.ReadLine()?.Trim();



    // WARRIOR
    if (choice == "1")
    {
        // Create the game character
        var player = new Warrior();

        // Save the character in the database to get a PlayerId
        var playerDb = await playerService.CreateAsync(new PlayerDB
        {
            PlayerName = playerName,
            Class = "Warrior",
            Level = player.Lvl
        });

        player.PlayerId = playerDb.PlayerId;

        // Create the inventory row for this character
        player.InventoryLogic =
            await InventoryLogic.CreateAsync(
                playerService.Inventory,
                playerDb.PlayerId);

        // Starting items are added once, when the character is created
        await player.AddStartingItemsAsync();
        
        // SHOW STATS
        player.ShowStats();
        
        // CONTINUE?
        Console.WriteLine();

        Console.WriteLine("Do you want to continue? y/n");

        string? answer =
            Console.ReadLine()?.Trim().ToLower();
        
        // START GAME
        if (answer == "y")
        {
            await LogicForVisit.Run(player);

            Console.WriteLine();

            Console.WriteLine("Returning to class selection...");
        }
        // DON'T START GAME
        else if (answer == "n")
        {
            continue;
        }
        
        // INVALID ANSWER
        else
        {
            Console.WriteLine("Please enter y or n.");
        }
    }
    // EXIT GAME
    else if (choice == "0")
    {
        Console.WriteLine("Goodbye!");

        break;
    }
    // INVALID MENU CHOICE
    else
    {
        Console.WriteLine("Please make a valid choice.");
    }
}