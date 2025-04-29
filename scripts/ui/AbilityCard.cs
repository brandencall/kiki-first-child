using Godot;
using System;

public partial class AbilityCard : Button
{
    [Export]
    public Label AbilityName { get; set; }
    [Export]
    public RichTextLabel Description { get; set; }
    [Export]
    public TextureRect AbilityIcon { get; set; }
    [Export]
    public Label CurrentLevel { get; set; }

    public event Action<IAbility> AbilitySelected;

    private IAbility _ability;
    private ShaderMaterial _shaderAbilityIcon;
    private Vector2 _originalSize = new Vector2(400, 150);
    private Vector2 _scaledSize = new Vector2(430, 170);
    private Tween _shineTween;

    public override void _Ready()
    {
        _shaderAbilityIcon = AbilityIcon.Material as ShaderMaterial;
        this.MouseEntered += OnMouseEntered;
        this.MouseExited += OnMouseExited;
        this.Pressed += () => OnPickButtonPressed();
    }

    private void OnMouseEntered()
    {
        CustomMinimumSize = _scaledSize;
        if (_shaderAbilityIcon != null)
        {
            _shineTween = CreateTween();
            _shineTween.TweenProperty(_shaderAbilityIcon, "shader_parameter/shine_progress", 1.0f, 1.5f)
                .From(0.0f);
        }
    }

    // Need to stop the tween on mouse exit.
    private void OnMouseExited()
    {
        CustomMinimumSize = _originalSize;
        _shineTween?.Kill();
        _shaderAbilityIcon.SetShaderParameter("shine_progress", 0.0f);
    }

    private void OnPickButtonPressed()
    {
        AbilitySelected?.Invoke(_ability);
    }

    public void SetAbility(IAbility ability)
    {
        _ability = ability;
        AbilityName.Text = ability.AbilityName;
        Description.Text = ability.Description;
        AbilityIcon.Texture = ability.AbilityIcon;
        CurrentLevel.Text = "Current Level: " + ability.CurrentLevel.ToString();

        if (_ability.OnLastLevel)
        {
            Disabled = true;
        }
    }
}
