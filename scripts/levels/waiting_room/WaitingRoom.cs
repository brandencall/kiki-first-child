using Godot;
using System;

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

	private SaveManager SaveManager => GetNode<SaveManager>("/root/SaveManager");

	public override void _Ready()
	{
		CharacterData character = SaveManager.DefaultCharacter;
		PackedScene characterScene = GD.Load<PackedScene>(character.Scene);
		var characterInstance = characterScene.Instantiate();
		AddChild(characterInstance);

		CharacterSelectArea.CharacterAreaEntered += OnCharacterSelectAreaEntered;
		CharacterSelectUI.CharacterSelected += OnCharacterSelected;
	}

	private void OnCharacterSelectAreaEntered()
	{
		//Display Character Selection scene.
		CharacterSelectUI.ShowCharacterSelection();
	}

	private void OnCharacterSelected(CharacterData character)
	{
		GD.Print("Character selected: " + character);
	}
		
}
