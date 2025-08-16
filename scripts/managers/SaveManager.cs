using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
	public const string SavePath = "user://savegame.json";
	public const string CharacterDataPath = "res://data/characters.json";
	public const string SkillTreeDataPath = "res://data/skillTrees.json";
	private List<CharacterData> _configuredCharacters;
	private List<SkillTreeData> _configuredSkillTrees;

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager");

	public override void _Ready()
	{
		var jsonCharacterData = FileAccess.Open(CharacterDataPath, FileAccess.ModeFlags.Read);
		var jsonSkillTreeData = FileAccess.Open(SkillTreeDataPath, FileAccess.ModeFlags.Read);
		string jsonCharacterText = jsonCharacterData.GetAsText();
		string jsonSkillTreeText = jsonSkillTreeData.GetAsText();
		_configuredCharacters = JsonSerializer.Deserialize<List<CharacterData>>(jsonCharacterText);
		_configuredSkillTrees = JsonSerializer.Deserialize<List<SkillTreeData>>(jsonSkillTreeText);
		LoadGame();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			GD.Print("Game is closing, saving game...");
			SaveGame();
			GetTree().Quit();
		}
	}

	public void LoadGame()
	{
		try
		{
			if (FileAccess.FileExists(SavePath))
			{
				using FileAccess file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
				string jsonString = file.GetAsText();
				GameState gameState = JsonSerializer.Deserialize<GameState>(jsonString) ?? new GameState();
				GameManager.Initialize(gameState);
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

	private void EnsureAllCharactersExist()
	{
		foreach (var character in _configuredCharacters)
		{
			if (!GameManager.GameStateData.Characters.Exists(c => c.Id == character.Id))
			{
				GameManager.GameStateData.Characters.Add(new CharacterData
				{
					Id = character.Id,
					IsUnlocked = character.IsUnlocked,
					Scene = character.Scene,
					IconPath = character.IconPath,
					Cost = character.Cost
				});
			}
		}
	}

	public void InitializeNewGame()
	{
		GameState newGameState = new();
		newGameState.Characters = _configuredCharacters;
		newGameState.SkillTrees = _configuredSkillTrees;
		newGameState.Schmeckels = 0;
		GameManager.Initialize(newGameState);
	}

	public void SaveGame()
	{
		try
		{
			GameManager.GameStateData.LastUsedCharacter = GameManager.CurrentCharacter.CharacterData;
			string jsonString = JsonSerializer.Serialize(GameManager.GameStateData, new JsonSerializerOptions { WriteIndented = true });
			using FileAccess file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
			file.StoreString(jsonString);
			GD.Print("Game saved successfully.");
		}
		catch (System.Exception e)
		{
			GD.PrintErr($"Failed to save game: {e.Message}");
		}
	}
}
