using Godot;

public partial class PurpleKnightPlayer : CharacterBody2D
{
    [Export]
    private HealthComponent _healthComponent;
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

        MoveAndSlide();
    }

    private void Die()
    {
        GD.Print("Player has died");
    }
}
