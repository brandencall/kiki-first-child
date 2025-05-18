using Godot;
using System;

public partial class Bullet : HitboxComponent
{
	[Export]
	public CollisionShape2D Collision { get; set; }
	[Export]
	public Sprite2D Sprite { get; set; }
	[Export]
	public float Speed = 700f;

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
		Sprite.Rotation = _direction.Angle();
		Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
		_currentSpeed = Speed;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += _direction.Normalized() * _currentSpeed * (float)delta;
	}

	private void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is HurtboxComponent hurtbox)
		{
			QueueFree();
		}
	}
}
