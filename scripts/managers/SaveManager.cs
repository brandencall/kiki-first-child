using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
	public CharacterData DefaultCharacter { get; private set; }
	public WeaponData DefaultWeapon { get; private set; }

	public const string SavePath = "user://savegame.json";
	private GameState _gameState = new();
	// Make this a config with a list of characters.
	private readonly List<CharacterData> _defaultCharacterList = new List<CharacterData>
	{
		new CharacterData { Id = "Test", IsUnlocked = true, Scene = "path/to/character", IconPath = "res://assets/PlaceholderAssets/Characters/Warrior_Blue_Icon.png" },
		new CharacterData { Id = "Test1", IsUnlocked = false, Scene = "path/to/character", IconPath = "res://assets/PlaceholderAssets/Characters/Warrior_Blue_Icon.png"}
	};
	// Make this a config with a list of weapons.
	private readonly List<WeaponData> _defaultWeaponList = new List<WeaponData>
	{
		new WeaponData{ Id = "Test", IsUnlocked = true, Scene = "path/to/weapon", IconPath = "res://assets/icon.svg" },
		new WeaponData { Id = "Test1", IsUnlocked = false, Scene = "path/to/weapon", IconPath = "res://assets/icon.svg"}
	};

	public override void _Ready()
	{
		DefaultCharacter = new CharacterData
		{
			Id = "Default",
			IsUnlocked = true,
			Scene = "res://scenes/player/blue_knight_player.tscn",
			IconPath = "res://assets/icon.svg"
		};
		DefaultWeapon = new WeaponData 
		{
			Id = "Default",
			IsUnlocked = true,
			Scene = "path/to/defaultScene",
			IconPath = "res://assets/icon.svg"
		};
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
		foreach (var defaultChar in _defaultCharacterList)
		{
			if (!_gameState.Characters.Exists(c => c.Id == defaultChar.Id))
			{
				_gameState.Characters.Add(new CharacterData
				{
					Id = defaultChar.Id,
					IsUnlocked = defaultChar.IsUnlocked,
					Scene = defaultChar.Scene
				});
			}
		}
	}

	private void EnsureAllWeaponsExist()
	{
		foreach (var defaultWep in _defaultWeaponList)
		{
			if (!_gameState.Characters.Exists(c => c.Id == defaultWep.Id))
			{
				_gameState.Characters.Add(new CharacterData
				{
					Id = defaultWep.Id,
					IsUnlocked = defaultWep.IsUnlocked,
					Scene = defaultWep.Scene
				});
			}
		}
	}

	public void InitializeNewGame()
	{
		_gameState.Characters = new List<CharacterData>(_defaultCharacterList);
		_gameState.Weapons = new List<WeaponData>(_defaultWeaponList);
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
