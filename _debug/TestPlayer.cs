using Godot;

public partial class TestPlayer : BaseEnemy
{
    [Export]
    private VelocityComponent _velocityComponent;
    [Export]
    private PathfindComponent _pathfindComponent;
    [Export]
    private Timer _pathTimer;

    public override void _Ready()
    {
        base._Ready();
        _pathTimer.Timeout += UpdatePath;
    }

    private void UpdatePath()
    {
        if (_player != null)
        {
            _pathfindComponent.SetTargetPosition(_player.GlobalPosition);
            _pathfindComponent.FollowPath(GlobalPosition);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _velocityComponent.Move(this);
    }
}
