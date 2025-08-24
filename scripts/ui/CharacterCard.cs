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
    private ShaderMaterial _characterIcon;
    private Tween _shineTween;
    private GameManager GameManager => GetNode<GameManager>("/root/GameManager");

    public override void _Ready()
    {
        CharacterButton.Pressed += OnCharacterSelected;
        BuyButton.Pressed += OnBuyButtonPressed;
        CharacterButton.MouseEntered += OnMouseEntered;
        CharacterButton.MouseExited += OnMouseExited;
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
        SetupCharacterButton();
        if (character.IsUnlocked == false)
        {
            BuyButton.Text = "Buy $" + character.Cost;
        }
        else
        {
            BuyButton.Visible = false;
        }
    }

    private void SetupCharacterButton()
    {
        Texture2D spriteTexture = ResourceLoader.Load<Texture2D>(_character.IconPath);
        if (CharacterSprite != null && spriteTexture != null)
        {
            SetupCharacterShader(spriteTexture);
        }
        else
        {
            GD.PrintErr($"Failed to load character icon or CharacterSprite is null for character: {_character.Id}");
        }
    }

    private void SetupCharacterShader(Texture2D spriteTexture)
    {
        CharacterSprite.Texture = spriteTexture;
        _characterIcon = CharacterSprite.Material.Duplicate() as ShaderMaterial;
        CharacterSprite.Material = _characterIcon;
        if (_characterIcon == null)
        {
            GD.PrintErr($"CharacterSprite for '{_character.Id}' does not have a ShaderMaterial assigned. Shine effect will not work. Please assign shader to its Material property.");
        }
        else
        {
            // Initialize shine_progress to 0.0 to ensure no shine initially
            _characterIcon.SetShaderParameter("shine_progress", 0.0f);
        }
    }

    private void OnMouseEntered()
    {
        if (_characterIcon != null)
        {
            _shineTween?.Kill();
            _shineTween = CreateTween();
            _shineTween.SetTrans(Tween.TransitionType.Quad)
                       .SetEase(Tween.EaseType.Out)
                       .TweenProperty(_characterIcon, "shader_parameter/shine_progress", 1.0f, 1.5f);
        }
    }

    private void OnMouseExited()
    {
        if (_characterIcon == null) return;
        _shineTween?.Kill();
        _shineTween = CreateTween();
        _shineTween.SetTrans(Tween.TransitionType.Quad)
                   .SetEase(Tween.EaseType.In)        
                   .TweenProperty(_characterIcon, "shader_parameter/shine_progress", 0.0f, 0.5f);
    }
}
