using Godot;

/*
 *  Load All Charaters (some will be locked in the future)
 *  Load all weapons for that charater (some will be locked in the future)
 *  Load Skill tree for the charater
 *  Load all Levels (some will be locked in the future)
 */
public partial class WaitingRoom : Node2D
{
	[Export]
	public CharacterSelectArea CharacterSelectArea { get; set; }
	[Export]
	public CharacterSelection CharacterSelectUI { get; set; }

	private BaseCharacter _currentCharacter;
	private bool _showCharacterSelection = true;
	private SaveManager SaveManager => GetNode<SaveManager>("/root/SaveManager");

	public override void _Ready()
	{
		CharacterData character = SaveManager.CurrentCharacter;
		PackedScene characterScene = GD.Load<PackedScene>(character.Scene);
		var characterInstance = characterScene.Instantiate<BaseCharacter>();
		_currentCharacter = characterInstance;
		AddChild(characterInstance);

		CharacterSelectArea.CharacterAreaEntered += OnCharacterSelectAreaEntered;
		CharacterSelectArea.CharacterAreaExited += OnCharacterSelectAreaExited;
		CharacterSelectUI.CharacterSelected += OnCharacterSelected;
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
		Vector2 position = _currentCharacter.GlobalPosition;
		RemoveChild(_currentCharacter);
		PackedScene characterScene = GD.Load<PackedScene>(character.Scene);
		_currentCharacter = characterScene.Instantiate<BaseCharacter>();
		_currentCharacter.GlobalPosition = position;
		AddChild(_currentCharacter);
		SaveManager.CurrentCharacter = character;
		_showCharacterSelection = false;
	}

}
