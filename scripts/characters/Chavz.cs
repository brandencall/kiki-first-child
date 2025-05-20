using Godot;
using System;

public partial class Chavz : BaseCharacter 
{
	[Export]
	public Steak Steak { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Steak.Initialize(this);
	}

	public override void OnBaseAttackTimerTimeout()
	{
		base.OnBaseAttackTimerTimeout();
		Steak.Attack();
	}
}
