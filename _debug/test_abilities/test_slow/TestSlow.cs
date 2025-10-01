using Godot;

public partial class TestSlow : Node2D
{
	[Export]
	public BaseCharacter Character { get; set; }
	[Export]
	public Area2D Upgrade { get; set; }

	public IAbility Slow { get; set; }
	public IAbility Poison { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Upgrade.AreaEntered += OnUpgradeAreaEntered;
		Slow = new SlowEffect();
		Poison = new PoisonEffect();
	}

	public void OnUpgradeAreaEntered(Area2D area)
	{
		Slow.Upgrade(Character);
		Poison.Upgrade(Character);
	}
}
