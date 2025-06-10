using Godot;

/*
 *  Load All Charaters (some will be locked in the future)
 *  Load Skill tree for the charater
 *  Load all Levels (some will be locked in the future)
 */
public partial class WaitingRoom : Node2D
{
	[Export]
	public CharacterSelectArea CharacterSelectArea { get; set; }
	[Export]
	public CharacterSelection CharacterSelectUI { get; set; }
	[Export]
	public LevelSelectArea LevelSelectArea { get; set; }
	
	[Signal]
	public delegate void LevelSelectedEventHandler();

	private bool _showCharacterSelection = true;
	private SceneManager SceneManager => GetNode<SceneManager>("/root/SceneManager"); 
	private GameManager GameManager => GetNode<GameManager>("/root/GameManager"); 

	public override void _Ready()
	{
		AddChild(GameManager.CurrentCharacter);

		CharacterSelectArea.CharacterAreaEntered += OnCharacterSelectAreaEntered;
		CharacterSelectArea.CharacterAreaExited += OnCharacterSelectAreaExited;
		CharacterSelectUI.CharacterSelected += OnCharacterSelected;

		LevelSelectArea.LevelAreaEntered += OnLevelAreaEntered;
	}

	private void OnCharacterSelectAreaEntered()
	{
		if (_showCharacterSelection)
		{
			CharacterSelectUI.ShowCharacterSelection();
		}
	}

	private void OnCharacterSelectAreaExited()
	{
		_showCharacterSelection = true;
	}

	private void OnCharacterSelected(CharacterData character)
	{
		Vector2 position = GameManager.CurrentCharacter.GlobalPosition;
		RemoveChild(GameManager.CurrentCharacter);
		PackedScene characterScene = GD.Load<PackedScene>(character.Scene);
		GameManager.CurrentCharacter = characterScene.Instantiate<BaseCharacter>();
		GameManager.CurrentCharacter.GlobalPosition = position;
		GameManager.CurrentCharacter.Initialize(character);
		AddChild(GameManager.CurrentCharacter);
		_showCharacterSelection = false;
	}

	// Could have player choose from multiple levels but for now just entering open world level.
	// We would create level data similar to the character data. It would contain the path to the Scene, 
	// path to the wave data and any other information related to the level.
	private void OnLevelAreaEntered()
	{
		RemoveChild(GameManager.CurrentCharacter);
		SceneManager.ChangeScene("res://scenes/game_world.tscn");
		// Hard coding current level. This will change when we have the player choose a level.
		LevelData levelData = new()
		{
			Id = "test",
			IsUnlocked = true,
			Scene = "res://scenes/levels/open_world_test.tscn"
		};
			
		SceneManager.CurrentLevel = levelData;
		EmitSignal(SignalName.LevelSelected);
	}

}
