using Godot;

public partial class TorchEnemy : CharacterBody2D
{
    [Export]
    private VelocityComponent _velocityComponent;
    [Export]
    private PathfindComponent _pathfindComponent;
    [Export]
    private HealthComponent _healthComponent;

    private CharacterBody2D _player;

    public override void _Ready()
    {
        CollisionLayer = 16;
        CollisionMask = 0 | 16;
        _player = this.GetPlayer();
        _healthComponent.Died += Die;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_player != null)
        {
            _pathfindComponent.SetTargetPosition(_player.GlobalPosition);
            _pathfindComponent.FollowPath();
            _velocityComponent.Move(this);
        }
    }

    private void Die()
    {
        GD.Print("Enemy has died");
        QueueFree();
    }
}
