using Godot;

public partial class PathfindComponent : Node2D
{
    [Export]
    private VelocityComponent _velocityComponent;
    public FlowFieldManager FlowField { get; set; }

    private Vector2 _targetDirection;
    private Vector2I _currentTile;
    private Vector2I _previousTile;


    public override void _Ready()
    {
        FlowField = this.GetFlowField();
        _currentTile = FlowField.ChunkManager.TileMap.LocalToMap(GlobalPosition);
    }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        _targetDirection = FlowField.GetFlowVector(targetPosition);
    }
/*
    
   NEED TO FIGURE OUT REGISTERING ENEMIES TO A TILE DOWN HERE!
 

 */
    public void FollowPath(Vector2 currentPosition)
    {
        _currentTile = FlowField.ChunkManager.TileMap.LocalToMap(GlobalPosition);
        if (_currentTile != _previousTile)
        {
            FlowField.UnRegisterTile(_previousTile);
            _previousTile = _currentTile;
            FlowField.RegisterCurrentTile(_currentTile);
        }
        var target = FlowField.GetFlowVector(currentPosition);
        if (target == Vector2.Zero)
        {
            return;
        }
        _velocityComponent.AccelerateInDirection(target);
    }

}
