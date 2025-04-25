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

    public override void _Ready()
    {
        this.Pressed += () => OnPickButtonPressed();
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
