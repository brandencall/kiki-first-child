using Godot;

public partial class PurpleKnightPlayer : CharacterBody2D
{
    [Export]
    public AnimatedSprite2D Animations { get; set; }
    [Export]
    private HealthComponent _healthComponent;
    [Export]
    private VelocityComponent _velocityComponent;

    public override void _Ready()
    {
        CollisionLayer = 8;
        CollisionMask = 0;
        AddToGroup("player");
        _healthComponent.Died += Die;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        _velocityComponent.AccelerateInDirection(inputDirection);
        if (inputDirection.X != 0)
        {
            if (inputDirection.X < 0)
            {
                Animations.FlipH = true;
            }
            else 
            {
                Animations.FlipH = false;
            }
        }

        if (inputDirection.IsZeroApprox())
        {
            _velocityComponent.Declerate();
            Animations.Play("idle");
        }
        else if (Velocity != Vector2.Zero)
        {
            Animations.Play("walk");
        }

        _velocityComponent.Move(this);
    }

    private void Die()
    {
        GD.Print("Player has died");
    }
}
