using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterCard : Control
{
	[Export]
	public TextureButton CharacterButton { get; set; }
	//[Export]
	//public Button ButtonTest { get; set; }
	[Export]
	public Label CharacterName { get; set; }

	public event Action<CharacterData> CharacterSelected;

	private CharacterData _character;

	public override void _Ready()
	{
	//	ButtonTest.Pressed += OnCharacterSelected;
		CharacterButton.Pressed += OnCharacterSelected;
	}

	private void OnCharacterSelected()
	{
		GD.Print("Char selected");
		CharacterSelected?.Invoke(_character);
	}

	public void SetCharacter(CharacterData character)
	{
		_character = character;
		CharacterName.Text = character.Id;
		SetupCharacterButton();
	}

	private void SetupCharacterButton()
	{
	//	ButtonTest.Icon = ResourceLoader.Load<Texture2D>(_character.IconPath);
	//	ButtonTest.Text = _character.Id;
	//	ButtonTest.Modulate = _character.IsUnlocked ? Colors.White : Colors.Gray;
	
		Texture2D spriteTexture = ResourceLoader.Load<Texture2D>(_character.IconPath);
		CharacterButton.TextureNormal = spriteTexture;
		CharacterButton.TextureClickMask = CreateClickMask(spriteTexture);
		CharacterButton.Modulate = _character.IsUnlocked ? Colors.White : Colors.Gray;
		CharacterButton.Disabled = !_character.IsUnlocked;
	//	GD.Print("Id: " + _character.Id + " Disabled: " + CharacterButton.Disabled);
	}

	private Bitmap CreateClickMask(Texture2D spriteTexture)
	{
		// Get the sprite's image
		Image image = spriteTexture.GetImage();
		if (image == null)
		{
			GD.PrintErr("Failed to get image from texture");
			return new Bitmap();
		}

		// Create a BitMap for the click mask
		Bitmap clickMask = new Bitmap();
		clickMask.CreateFromImageAlpha(image, 0.1f); // Use alpha threshold to define shape

		return clickMask;
	}
}
