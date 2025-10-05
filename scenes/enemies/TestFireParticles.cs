using Godot;

public partial class TestFireParticles : BaseEnemy
{
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;
		velocity.X = 100;
		velocity.Y = -100;

		// Apply movement
		Velocity = velocity;
		MoveAndSlide();
	}
}
