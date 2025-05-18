using Godot;

public partial class BaseCharacter : CharacterBody2D
{
	public enum Direction 
	{
		Right,
		Left,
		Up,
		Down
	}

	[Export]
	public AnimatedSprite2D Animations { get; set; }
	[Export]
	public VelocityComponent VelocityComponent { get; set; }

	public Direction facingDirection = Direction.Right;

	[Export]
	private HealthComponent _healthComponent;
	[Export]
	private float _baseAttackCooldown = 1.5f;

	private CollisionShape2D _hitboxCollision;
	private bool _isAttacking = false;

	[Signal]
	public delegate void ExperiencePickedupEventHandler(float experience);
	[Signal]
	public delegate void OnCharacterDiedEventHandler();

	public override void _Ready()
	{
		CollisionLayer = 8;
		CollisionMask = 0;
		AddToGroup("character");
		_healthComponent.Died += Die;
		GetNode<Area2D>("PickupArea").AreaEntered += OnPickupAreaEntered;

		Timer baseAttackTimer = new Timer();
		AddChild(baseAttackTimer);
		baseAttackTimer.WaitTime += _baseAttackCooldown;
		baseAttackTimer.OneShot = false;
		baseAttackTimer.Timeout += OnBaseAttackTimerTimeout;
		baseAttackTimer.Start();

		GodotUtilities.RegisterCharacter(this);
	}

	public virtual void OnBaseAttackTimerTimeout()
	{
		_isAttacking = true;
		Animations.Play("attack");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		VelocityComponent.AccelerateInDirection(inputDirection);
		if (inputDirection.X != 0)
		{
			if (inputDirection.X < 0)
			{
				Animations.FlipH = true;
				facingDirection = Direction.Left;
			}
			else
			{
				Animations.FlipH = false;
				facingDirection = Direction.Right;
			}
		}

		if (inputDirection.IsZeroApprox() && !_isAttacking)
		{
			VelocityComponent.Declerate();
			Animations.Play("idle");
		}
		else if (Velocity != Vector2.Zero && !_isAttacking)
		{
			Animations.Play("walk");
		}

		VelocityComponent.Move(this);
	}

	//TODO: Handle player dying
	private void Die()
	{
		GD.Print("Player has died");
		EmitSignal(SignalName.OnCharacterDied);
	}
	
	public void OnAnimatedSprite2dAnimationFinished()
	{
		if (Animations.Animation == "attack")
		{
			_isAttacking = false;
		}
	}

	//TODO: add some pickup logic
	private void OnPickupAreaEntered(Area2D area)
	{
		if (area is IExperience item)
		{
			EmitSignal(SignalName.ExperiencePickedup, item.Experience);
			item.QueueFree();
		}
	}
}
