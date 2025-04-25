using Godot;
using System;

public partial class AbilityUi : Control
{
    [Export]
    public VBoxContainer AbilityContainer { get; set; }
    [Export]
    public AnimationPlayer Animations { get; set; }

    public event Action<IAbility> AbilitySelected;

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

    public void AddAbilityCard(IAbility ability)
    {
        var card = _abilityCardScene.Instantiate<AbilityCard>();
        card.SetAbility(ability);
        card.AbilitySelected += OnAbilitySelected;
        AbilityContainer.AddChild(card);
    }

    public void OnAbilitySelected(IAbility selectedAbility)
    {
        AbilitySelected?.Invoke(selectedAbility);
    }
}
