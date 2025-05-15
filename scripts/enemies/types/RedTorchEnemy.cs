using Godot;

public partial class RedTorchEnemy : BaseEnemy 
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
        //_pathTimer.Timeout += UpdatePath;
    }

    private void UpdatePath()
    {
        if (_character != null)
        {
            _pathfindComponent.SetTargetPosition(_character.GlobalPosition);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _pathfindComponent.FollowPath(GlobalPosition);
        _velocityComponent.Move(this);
    }

}
