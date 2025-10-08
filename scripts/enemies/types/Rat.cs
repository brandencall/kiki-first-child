using Godot;

public partial class Rat : BaseEnemy 
{
	[Export]
	private VelocityComponent _velocityComponent;
	[Export]
	private PathfindComponent _pathfindComponent;
	[Export]
	private HealthComponent _healthComponent;
	[Export]
	private EnemyHitbox _hitboxComponent;


	public override void _Ready()
	{
		base._Ready();
		_healthComponent.Died += Die;
		_hitboxComponent.OwnerEntity = this;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		_pathfindComponent.FollowPath(GlobalPosition);
		_velocityComponent.Move(this);
	}

}
