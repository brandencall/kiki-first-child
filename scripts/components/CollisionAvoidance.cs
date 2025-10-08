using Godot;
using System.Collections.Generic;

public partial class CollisionAvoidance : Area2D
{
    [Export]
    public float RepulsionWeight { get; set; } = 0.3f;
    [Export]
    public float FlowFieldWeight { get; set; } = 0.7f;

    private List<CollisionAvoidance> _neighbors = new();

    public override void _Ready()
    {
        Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
        Connect("area_exited", new Callable(this, nameof(OnAreaExit)));
    }

    public Vector2 CalculateRepulsionVectors()
    {
        Vector2 totalRepulse = Vector2.Zero;
        foreach (var neighbor in _neighbors)
        {
            if (!Godot.GodotObject.IsInstanceValid(neighbor))
            {
                continue;
            }
            Vector2 repulseDirection = GlobalPosition - neighbor.GlobalPosition;
            float distance = GlobalPosition.DistanceTo(neighbor.GlobalPosition);
            Vector2 repulseVector = repulseDirection / distance;
            totalRepulse += repulseVector;
        }
        return totalRepulse == Vector2.Zero ? Vector2.Zero : totalRepulse.Normalized();
    }

    private void OnAreaEntered(Area2D otherArea)
    {
        if (otherArea is CollisionAvoidance avoidance)
        {
            _neighbors.Add(avoidance);
        }
    }

    private void OnAreaExit(Area2D otherArea)
    {
        if (otherArea is CollisionAvoidance avoidance)
        {
            _neighbors.Remove(avoidance);
        }
    }

}
