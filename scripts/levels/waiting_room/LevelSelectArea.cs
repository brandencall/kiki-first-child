using Godot;

public partial class LevelSelectArea : Area2D
{
	[Signal]
	public delegate void LevelAreaEnteredEventHandler();
	[Signal]
	public delegate void LevelAreaExitedEventHandler();

	public override void _Ready()
	{
		CollisionMask = 8;
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		EmitSignal(SignalName.LevelAreaEntered);
	}

	private void OnBodyExited(Node2D body)
	{
		EmitSignal(SignalName.LevelAreaExited);
	}
}
