using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SaveManager : Node
{
	public const string SavePath = "user://savegame.json";
	public const string CharacterDataPath = "res://data/characters.json";
	public const string SkillTreeDataPath = "res://data/skillTrees.json";
	private GameState _gameState = new();
	private List<CharacterData> _characterList;
	private List<SkillTreeData> _skillTreeData;

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager");

	public override void _Ready()
	{
		var jsonCharacterData = FileAccess.Open(CharacterDataPath, FileAccess.ModeFlags.Read);
		var jsonSkillTreeData = FileAccess.Open(SkillTreeDataPath, FileAccess.ModeFlags.Read);
		string jsonCharacterText = jsonCharacterData.GetAsText();
		string jsonSkillTreeText = jsonSkillTreeData.GetAsText();
		_characterList = JsonSerializer.Deserialize<List<CharacterData>>(jsonCharacterText);
		_skillTreeData = JsonSerializer.Deserialize<List<SkillTreeData>>(jsonSkillTreeText);
		LoadGame();
		GameManager.Initialize();
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
				_gameState = JsonSerializer.Deserialize<GameState>(jsonString) ?? new GameState();
				EnsureAllCharactersExist();
				AssignSkillTrees();
				PopulateGameManager();
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
		foreach (var character in _characterList)
		{
			if (!_gameState.Characters.Exists(c => c.Id == character.Id))
			{
				_gameState.Characters.Add(new CharacterData
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

	private void AssignSkillTrees()
	{
		foreach (CharacterData character in _characterList)
		{
			SkillTreeData skillTree = _skillTreeData.Find(s => s.CharacterId == character.Id);
		}
	}

	private void PopulateGameManager()
	{
		GameManager.CharacterDataList = _characterList;
		GameManager.GameStateData = _gameState;
	}

	public void SaveGame()
	{
		try
		{
			_gameState.LastUsedCharacter = GameManager.CurrentCharacter.CharacterData;
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

	public void InitializeNewGame()
	{
		_gameState.Characters = _characterList;
		_gameState.SkillTrees = _skillTreeData;
		AssignSkillTrees();
		PopulateGameManager();
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
