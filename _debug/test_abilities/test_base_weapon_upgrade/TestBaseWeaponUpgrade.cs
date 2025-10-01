using Godot;

public partial class TestBaseWeaponUpgrade : Node2D
{
	[Export]
	public BaseCharacter Character { get; set; }
	[Export]
	public Area2D Upgrade { get; set; }

	public IAbility Ability { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Upgrade.AreaEntered += OnUpgradeAreaEntered;
		Ability = new BaseAttackUpgrade();
	}

	public void OnUpgradeAreaEntered(Area2D area)
	{
		Ability.Upgrade(Character);
	}
}
