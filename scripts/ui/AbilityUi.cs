using Godot;

public partial class AbilityUi : Control
{
    [Export]
    public VBoxContainer AbilityContainer { get; set; }
    [Export]
    public AnimationPlayer Animations { get; set; }

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
        Animations.Play("blur");
    }

    public void ClearAbilities()
    {
        Visible = false;
        GetTree().Paused = false;
        Animations.PlayBackwards("blur");
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
