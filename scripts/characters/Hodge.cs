using Godot;
using System;

public partial class Hodge : BaseCharacter
{
	[Export]
	public Noodle Noodle { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Noodle.Initialize(this);
	}

	public override void OnBaseAttackTimerTimeout()
	{
		base.OnBaseAttackTimerTimeout();
		Noodle.Attack();
	}
}
