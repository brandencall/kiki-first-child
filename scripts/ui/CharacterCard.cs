using Godot;
using System;

public partial class CharacterCard : Control
{
	[Export]
	public TextureButton CharacterButton { get; set; }
	[Export]
	public Label CharacterName { get; set; }
	[Export]
	public Button BuyButton { get; set; }

	public event Action<CharacterData> CharacterSelected;

	private CharacterData _character;
	private GameManager GameManager => GetNode<GameManager>("/root/GameManager");

	public override void _Ready()
	{
		CharacterButton.Pressed += OnCharacterSelected;
		BuyButton.Pressed += OnBuyButtonPressed;
	}

	public void UpdateCardInfo(CharacterData characterData)
	{
		SetCharacter(characterData);
	}

	private void OnCharacterSelected()
	{
		CharacterSelected?.Invoke(_character);
	}

	private void OnBuyButtonPressed()
	{
		if (_character.Cost < GameManager.CurrencyBalance())
		{
			GameManager.WithdrawCurrency(_character.Cost);
			_character.IsUnlocked = true;
			OnCharacterSelected();
		}
	}

	public void SetCharacter(CharacterData character)
	{
		_character = character;
		CharacterName.Text = character.Id;
		if (character.IsUnlocked == false)
		{
			BuyButton.Text = "Buy $" + character.Cost;
		} else 
		{
			BuyButton.Visible = false;
		}
		SetupCharacterButton();
	}

	private void SetupCharacterButton()
	{
		Texture2D spriteTexture = ResourceLoader.Load<Texture2D>(_character.IconPath);
		CharacterButton.TextureNormal = spriteTexture;
		CharacterButton.TextureClickMask = CreateClickMask(spriteTexture);
		CharacterButton.Modulate = _character.IsUnlocked ? Colors.White : Colors.Gray;
		CharacterButton.Disabled = !_character.IsUnlocked;
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
