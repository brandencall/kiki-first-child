using Godot;

public partial class TNTEnemy : BaseEnemy
{
    [Export]
    private VelocityComponent _velocityComponent;
    [Export]
    private PathfindComponent _pathfindComponent;
    [Export]
    private HealthComponent _healthComponent;
    [Export]
    private Timer _pathTimer;

    public override void _Ready()
    {
        base._Ready();
        _healthComponent.Died += Die;
        _pathTimer.Timeout += UpdatePath;
    }

    private void UpdatePath()
    {
        if (_player != null)
        {
            _pathfindComponent.SetTargetPosition(_player.GlobalPosition);
            _pathfindComponent.FollowPath();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _velocityComponent.Move(this);
    }

}
