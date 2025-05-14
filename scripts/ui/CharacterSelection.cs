using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterSelection : Control
{
	[Export]
	public HBoxContainer CharacterContainer { get; set; }
	private SaveManager SaveManager => GetNode<SaveManager>("/root/SaveManager");

	public event Action<CharacterData> CharacterSelected;

	private PackedScene _characterCardScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/character_card.tscn");

	public override void _Ready()
	{
		Visible = false;
		CreateCharacterCards();
	}

	private void CreateCharacterCards()
	{
		List<CharacterData> characters = SaveManager.GetAllCharacters();
		foreach (var character in characters)
		{
			AddCharacterCard(character);
		}
	}

	public void ShowCharacterSelection()
	{
		Visible = true;
		GetTree().Paused = true;
	}

	public void HideCharacterSelection()
	{
		Visible = false;
		GetTree().Paused = false;
	}

	private void AddCharacterCard(CharacterData characterData)
	{
		var card = _characterCardScene.Instantiate<CharacterCard>();
		card.SetCharacter(characterData);
		card.CharacterSelected += OnCharacterSelected;
		CharacterContainer.AddChild(card);
	}

	private void OnCharacterSelected(CharacterData character)
	{
		CharacterSelected?.Invoke(character);
		HideCharacterSelection();
	}
}
