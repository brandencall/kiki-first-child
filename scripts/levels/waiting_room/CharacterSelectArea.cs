using Godot;

public partial class CharacterSelectArea : Area2D
{
	[Signal]
	public delegate void CharacterAreaEnteredEventHandler();
	[Signal]
	public delegate void CharacterAreaExitedEventHandler();

	public override void _Ready()
	{
		CollisionMask = 8;
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		EmitSignal(SignalName.CharacterAreaEntered);
	}

	private void OnBodyExited(Node2D body)
	{
		EmitSignal(SignalName.CharacterAreaExited);
	}

}
