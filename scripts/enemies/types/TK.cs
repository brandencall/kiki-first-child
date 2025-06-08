using Godot;

public partial class TK : BaseEnemy 
{
	[Export]
	private VelocityComponent _velocityComponent;
	[Export]
	private PathfindComponent _pathfindComponent;
	[Export]
	private HealthComponent _healthComponent;
	[Export]
	private AnimatedSprite2D _animations;

	private Timer _charaterDistanceTimer;

	public override void _Ready()
	{
		base._Ready();
		_healthComponent.Died += Die;

		_charaterDistanceTimer = new Timer();
		_charaterDistanceTimer.WaitTime = 1.0f;
		_charaterDistanceTimer.Timeout += CheckDistance;
		AddChild(_charaterDistanceTimer);
		_charaterDistanceTimer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		_pathfindComponent.FollowPath(GlobalPosition);
		_velocityComponent.Move(this);
	}

	private void CheckDistance()
	{
		float distance = GlobalPosition.DistanceTo(_character.GlobalPosition);
		if (distance < 300)
		{
			_animations.Play("agro");
		} else {
			_animations.Play("default");
		}
	}

	

}
