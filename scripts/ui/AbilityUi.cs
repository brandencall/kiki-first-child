using Godot;

public partial class AbilityUi : Control
{
    [Export]
    public VBoxContainer AbilityContainer { get; set; }

    [Signal]
    public delegate void AbilitySelectedEventHandler(AbilityResource ability);

    private PackedScene _abilityCardScene = ResourceLoader.Load<PackedScene>("res://scenes/ui/ability_card.tscn");

    public override void _Ready()
    {
        Visible = false;
    }

    public void ShowAbilities()
    {
        Visible = true;
        GetTree().Paused = true;
    }

    public void ClearAbilities()
    {
        Visible = false;
        GetTree().Paused = false;
        foreach (Node child in AbilityContainer.GetChildren())
        {
            child.QueueFree();
        }
    }


    public void AddAbilityCard(AbilityResource ability)
    {
        var card = _abilityCardScene.Instantiate<AbilityCard>();
        card.SetAbility(ability);
        card.AbilitySelected += OnAbilitySelected;
        AbilityContainer.AddChild(card);
    }

    public void OnAbilitySelected(AbilityResource selectedAbility)
    {
        EmitSignal(SignalName.AbilitySelected, selectedAbility);
    }
}
