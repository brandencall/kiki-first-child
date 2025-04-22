using Godot;

public partial class PathfindComponent : Node2D
{
    public NavigationAgent2D NavigationAgent2D { get; private set; }

    [Export]
    private VelocityComponent _velocityComponent;

    public async override void _Ready()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        NavigationAgent2D = GetNode<NavigationAgent2D>("NavigationAgent2D");
        NavigationAgent2D.AvoidanceEnabled = true;
        NavigationAgent2D.Radius = 40;
        NavigationAgent2D.TargetDesiredDistance = 50;
        NavigationAgent2D.VelocityComputed += OnVelocityComputed;
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        NavigationAgent2D.TargetPosition = targetPosition;
    }

    public void FollowPath()
    {
        if (NavigationServer2D.MapGetIterationId(NavigationAgent2D.GetNavigationMap()) == 0)
        {
            return;
        }
        if (NavigationAgent2D.IsNavigationFinished())
        {
            _velocityComponent.Declerate();
            return;
        }

        Vector2 currentPosition = GlobalPosition;
        Vector2 nextPathPostion = NavigationAgent2D.GetNextPathPosition();
        _velocityComponent.AccelerateInDirection(currentPosition.DirectionTo(nextPathPostion));
        NavigationAgent2D.SetVelocity(_velocityComponent.Velocity);
    }

    private void OnVelocityComputed(Vector2 safeVelocity)
    {
        _velocityComponent.AccelerateToVelocity(safeVelocity);
    }

}
