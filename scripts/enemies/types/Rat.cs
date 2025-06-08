using Godot;

public partial class Rat : BaseEnemy 
{
	[Export]
	private VelocityComponent _velocityComponent;
	[Export]
	private PathfindComponent _pathfindComponent;
	[Export]
	private HealthComponent _healthComponent;

	public override void _Ready()
	{
		base._Ready();
		_healthComponent.Died += Die;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		_pathfindComponent.FollowPath(GlobalPosition);
		_velocityComponent.Move(this);
	}

}
