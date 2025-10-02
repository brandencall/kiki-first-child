using Godot;

public partial class TestTooFastTooFurious : Node2D
{
	[Export]
	public BaseCharacter Character { get; set; }
	[Export]
	public Area2D Upgrade { get; set; }

	public IAbility TooFast { get; set; }
	public IAbility FireEffect { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Upgrade.AreaEntered += OnUpgradeAreaEntered;
		TooFast = new TooFastTooFurious();
		FireEffect = new FireEffect();
	}

	public void OnUpgradeAreaEntered(Area2D area)
	{
		TooFast.Upgrade(Character);
		FireEffect.Upgrade(Character);
	}
}
