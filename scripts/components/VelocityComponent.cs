using Godot;

public partial class VelocityComponent : Node
{
    [Export]
    public float MaxSpeed { get; set; } = 100f;
    [Export]
    private float _accelerationCoefficient = 2f;

	public Vector2 LastMoveDirection { get; private set; } = Vector2.Right; 
    public float AccelerationCoefficientMultiplier { get; set; } = 1f;
    public float AccelerationCoefficient => _accelerationCoefficient;
    public Vector2 Velocity { get; set; }

    public void AccelerateToVelocity(Vector2 velocity)
    {
        float delta = (float)GetPhysicsProcessDeltaTime();
        Velocity = Velocity.Lerp(velocity, 1f - Mathf.Exp(-_accelerationCoefficient * AccelerationCoefficientMultiplier * delta));
    }

    public void AccelerateInDirection(Vector2 direction)
    {
        AccelerateToVelocity(direction * MaxSpeed);
    }

    public void Declerate()
    {
        AccelerateToVelocity(Vector2.Zero);
    }

    public void Move(CharacterBody2D characterBody)
    {
        characterBody.Velocity = Velocity;
        characterBody.MoveAndSlide();
		if (!Velocity.IsZeroApprox())
		{
			LastMoveDirection = Velocity;
		}
    }

    public float GetMaxSpeed()
    {
        return MaxSpeed;
    }
}
