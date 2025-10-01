using Godot;

public partial class Bottle : HitboxComponent
{
	[Export]
	public CollisionShape2D Collision { get; set; }
	[Export]
	public Sprite2D Sprite { get; set; }
	[Export]
	public float Speed = 700f;
	[Export]
	public float LifeTime = 2.0f;
	[Export]
	public float RotationSpeed = 180.0f;

	private BaseCharacter _character;
	private Vector2 _direction;
	private float _currentSpeed;

	public override void _Ready()
	{
		base._Ready();
		_character = this.GetCharacter();
		this.AreaEntered += OnAreaEntered;
		GlobalPosition = _character.GlobalPosition;
		_direction = _character.VelocityComponent.LastMoveDirection.Normalized();
		_currentSpeed = Speed;

		Timer lifeTimer = new Timer();
		AddChild(lifeTimer);
		lifeTimer.WaitTime = LifeTime;
		lifeTimer.Timeout += OnLifeTimerTimeout;
		lifeTimer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += _direction * _currentSpeed * (float)delta;
		if (_direction.X > 0)
		{
			Rotation += Mathf.DegToRad(RotationSpeed) * (float)delta;
		}
		else
		{
			Rotation += Mathf.DegToRad(-RotationSpeed) * (float)delta;
		}
	}

	private void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is HurtboxComponent hurtbox)
		{
			QueueFree();
		}
	}

	private void OnLifeTimerTimeout()
	{
		QueueFree();
	}
}
