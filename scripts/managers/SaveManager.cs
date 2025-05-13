using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
    public const string SavePath = "user://savegame.json";
    private GameState _gameState = new();
    // Make this a config with a list of characters.
    private readonly List<CharacterData> _defaultCharacters = new List<CharacterData>
    {
        new CharacterData { Id = "Test", IsUnlocked = true, Path = "path/to/character" },
        new CharacterData { Id = "Test1", IsUnlocked = false, Path = "path/to/character" }
    }
    // Make this a config with a list of weapons.
    private readonly List<WeaponData> _defaultWeapons = new List<WeaponData>
    {
        new WeaponData{ Id = "Test", IsUnlocked = true, Path = "path/to/weapon" },
        new WeaponData { Id = "Test1", IsUnlocked = false, Path = "path/to/weapon" }
    }

    public override void _Ready()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        try
        {
            if (FileAccess.FileExists(SavePath))
            {
                using FileAccess file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
                string jsonString = file.GetAsText();
                _gameState = JsonSerializer.Deserialize<GameState>(jsonString) ?? new GameState();
                EnsureAllCharactersExist();
                EnsureAllWeaponsExist();
                GD.Print("Game loaded suxxessfully");
            }
            else
            {
                InitializeNewGame();
                GD.Print("No save file. Starting new game");
            }

        }
        catch (System.Exception e)
        {
            InitializeNewGame();
            GD.Print("Failed to load game: " + e.Message);
        }
    }

    public void SaveGame()
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(_gameState, new JsonSerializerOptions { WriteIndented = true });
            using FileAccess file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
            file.StoreString(jsonString);
            GD.Print("Game saved successfully.");
        }
        catch (System.Exception e)
        {
            GD.PrintErr($"Failed to save game: {e.Message}");
        }
    }

    private void EnsureAllCharactersExist()
    {
        foreach (var defaultChar in _defaultCharacters)
        {
            if (!_gameState.Characters.Exists(c => c.Id == defaultChar.Id))
            {
                _gameState.Characters.Add(new CharacterData
                {
                    Id = defaultChar.Id,
                    IsUnlocked = defaultChar.IsUnlocked,
                    Path = defaultChar.Path
                });
            }
        }
    }

    private void EnsureAllWeaponsExist()
    {
        foreach (var defaultWep in _defaultWeapons)
        {
            if (!_gameState.Characters.Exists(c => c.Id == defaultWep.Id))
            {
                _gameState.Characters.Add(new CharacterData
                {
                    Id = defaultWep.Id,
                    IsUnlocked = defaultWep.IsUnlocked,
                    Path = defaultWep.Path
                });
            }
        }
    }

    public void InitializeNewGame()
    {
        _gameState.Characters = new List<CharacterData>(_defaultCharacters);
        _gameState.Weapons = new List<WeaponData>(_defaultWeapons);
    }

    public void UnlockCharacter(string characterId)
    {
        CharacterData character = _gameState.Characters.Find(c => c.Id == characterId);
        if (character != null && !character.IsUnlocked)
        {
            character.IsUnlocked = true;
            SaveGame();
            GD.Print("Unloced character: " + characterId);
        }
    }

    public void UnlockWeapon(string weaponId)
    {
        CharacterData weapon = _gameState.Characters.Find(c => c.Id == weaponId);
        if (weapon != null && !weapon.IsUnlocked)
        {
            weapon.IsUnlocked = true;
            SaveGame();
            GD.Print("Unloced weapon: " + weaponId);
        }
    }

    public bool IsCharacterLocked(string characterId)
    {
        CharacterData character = _gameState.Characters.Find(c => c.Id == characterId);
        return character?.IsUnlocked ?? false;
    }

    public bool IsWeaponLocked(string weaponId)
    {
        CharacterData weapon = _gameState.Characters.Find(c => c.Id == weaponId);
        return weapon?.IsUnlocked ?? false;
    }

    public List<CharacterData> GetAllCharacters()
    {
        return _gameState.Characters;
    }

    public List<WeaponData> GetAllWeapons()
    {
        return _gameState.Weapons;
    }

}
