using Godot;

public partial class PurpleTorchEnemy : BaseEnemy 
{
    [Export]
    private VelocityComponent _velocityComponent;
    [Export]
    private PathfindComponent _pathfindComponent;
    [Export]
    private HealthComponent _healthComponent;

    public override void _Ready()
    {
        base._Ready();
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
}
