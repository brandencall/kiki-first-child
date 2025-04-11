using Godot;

public partial class VelocityComponent : Node
{
    [Export]
    private float _maxSpeed = 100f;
    [Export]
    private float _accelerationCoefficient = 2f;

    public float AccelerationCoefficientMultiplier { get; set; } = 1f;
    public float AccelerationCoefficient => _accelerationCoefficient;
    public Vector2 Velocity { get; set; }

    //TODO: Figure out the right way to do acceleration to velocity.
    public void AccelerateToVelocity(Vector2 velocity)
    {
        float delta = (float)GetPhysicsProcessDeltaTime();
        Velocity = Velocity.Lerp(velocity, (float)delta * _accelerationCoefficient);
    }

    public void AccelerateInDirection(Vector2 direction)
    {
        AccelerateToVelocity(direction * _maxSpeed);
    }

    public void Declerate()
    {
        AccelerateToVelocity(Vector2.Zero);
    }

    public void Move(CharacterBody2D characterBody)
    {
        characterBody.Velocity = Velocity;
        characterBody.MoveAndSlide();
    }

    public float GetMaxSpeed()
    {
        return _maxSpeed;
    }
}
