using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public BasePlayerStats Stats { get; set; }
    [Export]
    private Camera2D _camera;
    [Signal]
    public delegate void PlayerMovedEventHandler(Vector2 newPosition);
    [Signal]
    public delegate void PlayerDiedEventHandler();

    public override void _Ready()
    {
        AddToGroup("players");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = inputDirection * Stats.Speed;
        MoveAndSlide();
        EmitSignal(SignalName.PlayerMoved, GlobalPosition);
    }

    public void TakeDamage(int damage)
    {
        GD.Print("Player took " + damage);
        Stats.Health -= damage;

        if (Stats.Health <= 0)
        {
            Die();
        }
    }

    public Vector2 GetCameraBounds()
    {
        var screenSize = GetViewportRect().Size;
        Vector2 zoomedSize = screenSize / _camera.Zoom;
        Vector2 topLeft = _camera.GlobalPosition - zoomedSize / 2;
        return topLeft;
    }

    private void Die()
    {
        GD.Print("Player died");
        EmitSignal(SignalName.PlayerDied);
    }
}
