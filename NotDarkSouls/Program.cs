using LinqToDB;
using LinqToDB.Data;
using NotDarkSouls.Classes;
using NotDarkSouls.Classes.Map;
using NotDarkSouls.Classes.Map.Menu;
using NotDarkSouls.DataBase;
using NotDarkSouls.Service;


// ---------- Database ----------
using var db = new DataConnection(
    new DataOptions().UseSQLite("Data Source=notdarksouls.db"));

db.CreateTable<PlayerDB>(tableOptions: TableOptions.CheckExistence);
db.CreateTable<StatsDB>(tableOptions: TableOptions.CheckExistence);
db.CreateTable<InventoryDB>(tableOptions: TableOptions.CheckExistence);
db.CreateTable<InventoryItemDB>(tableOptions: TableOptions.CheckExistence);

// ---------- Services ----------
var statsService = new StatsService(db);
var itemService = new InventoryItemService(db);
var inventoryService = new InventoryService(db, itemService);
var playerService = new PlayerService(db, statsService, inventoryService);

// ---------- Start menu ----------
var startMenu = new StartMenu(playerService);

var result = await startMenu.StartAsync();

if (result.Action == StartAction.Quit)
    return;

BaseClass player = result.Action == StartAction.Continue
    ? await LoadCharacterAsync(result.Player!)
    : await CreateNewCharacterAsync();

player.ShowStats();

// ---------- Game loop ----------
player.ShowStats();

string startLocation = result.Action == StartAction.NewGame
    ? "Forrest"
    : result.Player!.Location;

await LogicForVisit.Run(player, startLocation, SaveLocationAsync);


// New character

async Task<BaseClass> CreateNewCharacterAsync()
{
    Console.Write("Enter a name for your character: ");
    string? name = Console.ReadLine()?.Trim();

    while (string.IsNullOrWhiteSpace(name))
    {
        Console.Write("Please enter a valid name: ");
        name = Console.ReadLine()?.Trim();
    }

    // The class decides the starting stats
    BaseClass character = ChooseClass();

    var playerRow = await playerService.CreateAsync(new PlayerDB
    {
        PlayerName = name,
        Class = character.ClassName,
        Level = character.Lvl
    });

    character.PlayerId = playerRow.PlayerId;

    // StatsService.CreateAsync sets PlayerId itself
    await playerService.Stats.CreateAsync(character.PlayerId, new StatsDB
    {
        Health = character.Health,
        Strength = character.Strength,
        Mana = character.Mana,
        Dexterity = character.Dexterity
    });

    // The inventory must be assigned before the starting items are added
    character.InventoryLogic =
        await InventoryLogic.CreateAsync(playerService.Inventory, character.PlayerId);

    await character.AddStartingItemsAsync();

    return character;
}

BaseClass ChooseClass()
{
    var names = ClassFactory.Names;

    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Choose a class:");

        for (int i = 0; i < names.Count; i++)
            Console.WriteLine($"[{i + 1}] {names[i]}");

        string? input = Console.ReadLine()?.Trim();

        if (int.TryParse(input, out int number) &&
            number >= 1 && number <= names.Count)
        {
            return ClassFactory.Create(names[number - 1])!;
        }

        Console.WriteLine("Please make a valid choice.");
    }
}



// Load existing character

async Task<BaseClass> LoadCharacterAsync(PlayerDB row)
{
    var character = ClassFactory.Create(row.Class)
        ?? throw new InvalidOperationException($"Unknown class '{row.Class}' in save.");

    var stats = await playerService.Stats.GetByPlayerIdAsync(row.PlayerId);

    character.PlayerId = row.PlayerId;
    character.Lvl = row.Level;

    // Only overwrite the class defaults if a stats row exists
    if (stats != null)
    {
        character.Health = stats.Health;
        character.Strength = stats.Strength;
        character.Mana = stats.Mana;
        character.Dexterity = stats.Dexterity;
    }

    // Loaded characters get their items from the database,
    // so AddStartingItemsAsync is NOT called here
    character.InventoryLogic =
        await InventoryLogic.LoadAsync(playerService.Inventory, row.PlayerId)
        ?? await InventoryLogic.CreateAsync(playerService.Inventory, row.PlayerId);

    return character;
}




// Save level and stats (inventory is saved on every change already)

async Task SaveCharacterAsync(BaseClass p)
{
    // Load the existing row first so the name and class aren't overwritten
    var row = await playerService.GetByIdAsync(p.PlayerId);

    if (row != null)
    {
        row.Level = p.Lvl;
        await playerService.UpdateAsync(p.PlayerId, row);
    }

    await playerService.Stats.UpdateAsync(p.PlayerId, new StatsDB
    {
        Health = p.Health,
        Strength = p.Strength,
        Mana = p.Mana,
        Dexterity = p.Dexterity
    });
}

// Save the current location

async Task SaveLocationAsync(string location)
{
    var row = await playerService.GetByIdAsync(player.PlayerId);

    if (row != null)
    {
        row.Location = location;
        await playerService.UpdateAsync(player.PlayerId, row);
    }
}