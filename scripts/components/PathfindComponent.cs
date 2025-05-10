using Godot;

public partial class PathfindComponent : Node2D
{
    [Export]
    private VelocityComponent _velocityComponent;
    [Export]
    private CollisionAvoidance _avoidance;
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
        Vector2 target;
        Vector2 flowFieldVector = FlowField.GetFlowVector(currentPosition);
        if (_avoidance != null)
        {
            Vector2 repulsionVector = _avoidance.RepulsionVector;
            target = _avoidance.FlowFieldWeight * flowFieldVector + _avoidance.RepulsionWeight * repulsionVector;
        }
        else
        {
            target = flowFieldVector;
        }
        if (target == Vector2.Zero)
        {
            return;
        }
        _velocityComponent.AccelerateInDirection(target);
    }

}
