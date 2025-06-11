using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterSelection : Control
{
	[Export]
	public HBoxContainer CharacterContainer { get; set; }
	private SaveManager SaveManager => GetNode<SaveManager>("/root/SaveManager");
	private GameManager GameManager => GetNode<GameManager>("/root/GameManager"); 

	public event Action<CharacterData> CharacterSelected;

	private PackedScene _characterCardScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/character_card.tscn");
	private Dictionary<CharacterData, CharacterCard> characterCardMap = new();

	public override void _Ready()
	{
		Visible = false;
		CreateCharacterCards();
	}

	private void CreateCharacterCards()
	{
		List<CharacterData> characters = GameManager.CharacterDataList;
		foreach (var character in characters)
		{
			AddCharacterCard(character);
		}
	}

	private void AddCharacterCard(CharacterData characterData)
	{
		var card = _characterCardScene.Instantiate<CharacterCard>();
		card.SetCharacter(characterData);
		card.CharacterSelected += OnCharacterSelected;
		CharacterContainer.AddChild(card);
		characterCardMap.Add(characterData, card);
	}

	public void ShowCharacterSelection()
	{
		Visible = true;
		ReloadCharacterData();
		GetTree().Paused = true;
	}

	private void ReloadCharacterData()
	{
		List<CharacterData> characters = GameManager.CharacterDataList;
		foreach (var character in characterCardMap)
		{
			UpdateCardInfo(character.Key, character.Value);
		}
	} 

	private void UpdateCardInfo(CharacterData characterData, CharacterCard card)
	{
		card.UpdateCardInfo(characterData);
	}


	private void OnCharacterSelected(CharacterData character)
	{
		CharacterSelected?.Invoke(character);
		HideCharacterSelection();
	}

	public void HideCharacterSelection()
	{
		Visible = false;
		GetTree().Paused = false;
	}

}
