using Godot;

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
        if (ability.AbilityLogic == null)
        {
            CurrentLevel.Text = "Current Level: 1"; 
        }
        else
        {
            CurrentLevel.Text = "Current Level: " + ability.AbilityLogic.CurrentLevel.ToString();
        }
    }
}
