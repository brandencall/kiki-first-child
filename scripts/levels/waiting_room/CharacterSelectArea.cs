using Godot;
using System;

public partial class CharacterSelectArea : Area2D
{
	[Signal]
	public delegate void CharacterAreaEnteredEventHandler();

	public override void _Ready()
	{
		CollisionMask = 8;
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		EmitSignal(SignalName.CharacterAreaEntered);
	}

}
