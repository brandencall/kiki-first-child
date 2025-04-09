using Godot;

public partial class PurpleKnightPlayer : CharacterBody2D
{
    [Export]
    private HealthComponent _healthComponent;
    [Export]
    public AnimatedSprite2D Animations { get; set; }

    public float Speed = 200;

    public override void _Ready()
    {
        AddToGroup("player");
        _healthComponent.Died += Die;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = inputDirection * Speed;
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

        if (Velocity == Vector2.Zero)
        {
            Animations.Play("idle");
        }
        else if (Velocity != Vector2.Zero)
        {
            Animations.Play("walk");
        }

        MoveAndSlide();
    }

    private void Die()
    {
        GD.Print("Player has died");
    }
}
