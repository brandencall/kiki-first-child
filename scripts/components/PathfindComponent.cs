using Godot;

public partial class PathfindComponent : Node2D
{
    [Export]
    private VelocityComponent _velocityComponent;
    public FlowFieldManager FlowField { get; set; }
    private Vector2 _targetDirection;


    public override void _Ready()
    {
        FlowField = this.GetFlowField();
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _targetDirection = FlowField.GetFlowVector(targetPosition);
    }

    public void FollowPath(Vector2 currentPosition)
    {
        var target = FlowField.GetFlowVector(currentPosition);
        if (target == Vector2.Zero)
        {
            return;
        }
        _velocityComponent.AccelerateInDirection(target);
    }

}
