using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
	public CharacterData DefaultCharacter { get; private set; }
	public const string SavePath = "user://savegame.json";
	private GameState _gameState = new();
	// Make this a config with a list of characters.
	private readonly List<CharacterData> _defaultCharacterList = new List<CharacterData>
	{
		new CharacterData 
		{ 
			Id = "Purple Knight",
			IsUnlocked = true,
			Scene = "res://scenes/player/purple_knight_player.tscn",
			IconPath = "res://assets/PlaceholderAssets/Characters/Warrior_Purple_Icon.png" 
		},
		new CharacterData 
		{ 
			Id = "Blue Knight",
			IsUnlocked = true,
			Scene = "res://scenes/player/blue_knight_player.tscn",
			IconPath = "res://assets/PlaceholderAssets/Characters/Warrior_Blue_Icon.png"
		},
		new CharacterData 
		{ 
			Id = "Test",
			IsUnlocked = false,
			Scene = "res://scenes/player/blue_knight_player.tscn",
			IconPath = "res://assets/PlaceholderAssets/Characters/Warrior_Blue_Icon.png"
		}
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

	public void InitializeNewGame()
	{
		_gameState.Characters = new List<CharacterData>(_defaultCharacterList);
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

	public bool IsCharacterLocked(string characterId)
	{
		CharacterData character = _gameState.Characters.Find(c => c.Id == characterId);
		return character?.IsUnlocked ?? false;
	}

	public List<CharacterData> GetAllCharacters()
	{
		return _gameState.Characters;
	}

}
