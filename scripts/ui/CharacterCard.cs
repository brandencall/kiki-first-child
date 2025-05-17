using Godot;
using System;

public partial class CharacterCard : Control
{
	[Export]
	public TextureButton CharacterButton { get; set; }
	[Export]
	public Label CharacterName { get; set; }

	public event Action<CharacterData> CharacterSelected;

	private CharacterData _character;

	public override void _Ready()
	{
		CharacterButton.Pressed += OnCharacterSelected;
	}

	private void OnCharacterSelected()
	{
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
		Texture2D spriteTexture = ResourceLoader.Load<Texture2D>(_character.IconPath);
		CharacterButton.TextureNormal = spriteTexture;
		CharacterButton.TextureClickMask = CreateClickMask(spriteTexture);
		CharacterButton.Modulate = _character.IsUnlocked ? Colors.White : Colors.Gray;
		CharacterButton.Disabled = !_character.IsUnlocked;
		// Scale the button once we get more of our custom characters in.
		//CharacterButton.Scale = new Vector2(3.0f, 3.0f);
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
