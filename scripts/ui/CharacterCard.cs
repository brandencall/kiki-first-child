using Godot;
using System;

public partial class CharacterCard : Control
{
    [Export]
    public Button CharacterButton { get; set; }
    [Export]
    public TextureRect CharacterSprite { get; set; }
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
        if (_character.IsUnlocked)
        {
            CharacterSelected?.Invoke(_character);
        }
    }

    private void OnBuyButtonPressed()
    {
        if (_character.Cost < GameManager.CurrencyBalance())
        {
            GameManager.WithdrawCurrency(_character.Cost);
            GameManager.UnlockCharacter(_character.Id);
            OnCharacterSelected();
        }
    }

    public void SetCharacter(CharacterData character)
    {
        _character = character;
        CharacterName.Text = _character.Id;
        SetupCharacterButton(character);
        if (character.IsUnlocked == false)
        {
            BuyButton.Text = "Buy $" + character.Cost;
        }
        else
        {
            BuyButton.Visible = false;
        }
    }

    private void SetupCharacterButton(CharacterData character)
    {
        Texture2D spriteTexture = ResourceLoader.Load<Texture2D>(_character.IconPath);
        if (CharacterSprite != null && spriteTexture != null)
        {
            CharacterSprite.Texture = spriteTexture;
        }
        else
        {
            GD.PrintErr($"Failed to load character icon or CharacterSprite is null for character: {_character.Id}");
        }
    }
}
