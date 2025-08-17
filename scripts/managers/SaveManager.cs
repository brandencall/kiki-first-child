using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
	public const string SavePath = "user://savegame.json";
	public const string CharacterDataPath = "res://data/characters.json";
	private List<CharacterData> _configuredCharacters;

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager");

	public override void _Ready()
	{
		var jsonCharacterData = FileAccess.Open(CharacterDataPath, FileAccess.ModeFlags.Read);
		string jsonCharacterText = jsonCharacterData.GetAsText();
		_configuredCharacters = JsonSerializer.Deserialize<List<CharacterData>>(jsonCharacterText);
		LoadGame();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
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
			}
			else
			{
				InitializeNewGame();
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
				GameManager.GameStateData.Characters.Add(character);
			}
		}
	}

	public void InitializeNewGame()
	{
		GameState newGameState = new();
		newGameState.Characters = _configuredCharacters;
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
		}
		catch (System.Exception e)
		{
			GD.PrintErr($"Failed to save game: {e.Message}");
		}
	}
}
