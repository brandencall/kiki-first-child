using Godot;

public partial class AbilityCard : Button 
{
    [Export]
    public Label AbilityName { get; set; }
    [Export]
    public RichTextLabel Description { get; set; }
    [Export]
    public TextureRect AbilityIcon { get; set; }

    [Signal]
    public delegate void AbilitySelectedEventHandler(AbilityResource ability);

    private AbilityResource _ability;

    public override void _Ready()
    {
        this.Pressed += () => OnPickButtonPressed();
    }

    private void OnPickButtonPressed()
    {
        EmitSignal(SignalName.AbilitySelected, _ability);
    }

    public void SetAbility(AbilityResource ability)
    {
        _ability = ability;
        AbilityName.Text = ability.AbilityName;
        Description.Text = ability.Description;
        AbilityIcon.Texture = ability.Icon;
    }

}
