using Godot;
using System.Diagnostics;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
	public CharacterData CurrentCharacter { get; set; }
	public const string SavePath = "user://savegame.json";
	public const string CharacterDataPath = "res://data/characters.json";
	private GameState _gameState = new();
	private List<CharacterData> _characterList;
	private string _defaultCharacterId = "Purple Knight";

	public override void _Ready()
	{
		var jsonFile = FileAccess.Open(CharacterDataPath, FileAccess.ModeFlags.Read);
		string jsonText = jsonFile.GetAsText();
		_characterList = JsonSerializer.Deserialize<List<CharacterData>>(jsonText);
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
				CurrentCharacter = _gameState.LastUsedCharacter;
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
		foreach (var character in _characterList)
		{
			if (!_gameState.Characters.Exists(c => c.Id == character.Id))
			{
				_gameState.Characters.Add(new CharacterData
				{
					Id = character.Id,
					IsUnlocked = character.IsUnlocked,
					Scene = character.Scene
				});
			}
		}
	}

	public void InitializeNewGame()
	{
		_gameState.Characters = _characterList;
		CurrentCharacter = GetCharacterById(_defaultCharacterId);
		Debug.Assert(CurrentCharacter != null, "The current character should not be null here");
	}

	public CharacterData GetCharacterById(string characterId)
	{
		CharacterData character = _gameState.Characters.Find(c => c.Id == characterId);
		return character;
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
