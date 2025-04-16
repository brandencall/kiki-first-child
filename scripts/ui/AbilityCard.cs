using Godot;

public partial class AbilityCard : VBoxContainer
{
    [Export]
    public Label AbilityName { get; set; }
    [Export]
    public RichTextLabel Description { get; set; }
    [Export]
    public TextureRect Icon { get; set; }
    [Export]
    public Button PickButton { get; set; }

    [Signal]
    public delegate void AbilitySelectedEventHandler(AbilityResource ability);

    private AbilityResource _ability;

    public override void _Ready()
    {
        PickButton.Pressed += () => OnPickButtonPressed();
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
        //Icon.Texture = ability.Icon;
    }

}
