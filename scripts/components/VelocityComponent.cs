using Godot;
using System.Collections.Generic;

public partial class VelocityComponent : Node
{
    [Export]
    public float BaseSpeed { get; set; } = 100f;
    [Export]
    private float _accelerationCoefficient = 2f;

    private List<float> _speedMultipliers = new();

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
        AccelerateToVelocity(direction.Normalized() * GetCurrentSpeed());
    }

    public float GetCurrentSpeed()
    {
        float finalSpeed = BaseSpeed;
        float totalMultiplier = 1f;
        foreach (var m in _speedMultipliers)
        {
            totalMultiplier *= m;
        }
        return finalSpeed * totalMultiplier;
    }

    public void AddSpeedMultiplier(float value)
    {
        _speedMultipliers.Add(value);
    }

    public void RemoveSpeedMultiplier(float value)
    {
        _speedMultipliers.Remove(value);
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

    public float GetBaseSpeed()
    {
        return BaseSpeed;
    }
}
