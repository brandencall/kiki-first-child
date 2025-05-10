using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class CollisionAvoidance : Area2D
{
	[Export]
	public float RepulsionWeight { get; set; } = 0.3f;
	[Export]
	public float FlowFieldWeight { get; set; } = 0.7f;
	public Vector2 RepulsionVector { get; set; }

	private List<CollisionAvoidance> _neighbors = new();
	private List<CollisionAvoidance> _neighborsToAdd = new();
	private List<CollisionAvoidance> _neighborsToRemove = new();

	public override void _Ready()
	{
		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
		Connect("area_exited", new Callable(this, nameof(OnAreaExit)));
	}

	public override void _PhysicsProcess(double delta)
	{
		ApplyNeighborChanges();
		CalculateRepulsionVectors();
	}

	private void ApplyNeighborChanges()
	{
		foreach (var neighbor in _neighborsToAdd)
		{
			_neighbors.Add(neighbor);
		}
		foreach (var neighbor in _neighborsToRemove)
		{
			_neighbors.Remove(neighbor);
		}
		_neighborsToAdd.Clear();
		_neighborsToRemove.Clear();
		_neighbors = _neighbors.Where(GodotObject.IsInstanceValid).ToList();
	}

	private void CalculateRepulsionVectors()
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
		RepulsionVector = totalRepulse == Vector2.Zero ? Vector2.Zero : totalRepulse.Normalized();
	}

	private void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is CollisionAvoidance avoidance)
		{
			_neighborsToAdd.Add(avoidance);
		}
	}

	private void OnAreaExit(Area2D otherArea)
	{
		if (otherArea is CollisionAvoidance avoidance)
		{
			_neighborsToRemove.Remove(avoidance);
		}
	}

}
