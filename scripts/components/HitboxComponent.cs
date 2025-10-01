using Godot;
using System.Collections.Generic;

public partial class HitboxComponent : Area2D
{
	[Export]
	public float Damage { get; set; } = 0f;
	public List<IEffect> Effects { get; set; } = new();

	public override void _Ready()
	{
		CollisionMask = 32;
		CollisionLayer = 2;
	}
}
