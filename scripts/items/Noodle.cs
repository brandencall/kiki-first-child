using Godot;
using System;

public partial class Noodle : HitboxComponent
{
	[Export]
	public AnimationPlayer Animation { get; set; }
	[Export]
	public Sprite2D Sprite { get; set; }

	private BaseCharacter _character;

	public void Initialize(BaseCharacter character)
	{
		_character = character;
	}

	public void Attack()
	{
		Rotation = _character.VelocityComponent.LastMoveDirection.Angle();
		if (_character.VelocityComponent.LastMoveDirection.X < 0)
		{
			Sprite.FlipV = true;
		}
		else
		{
			Sprite.FlipV = false;
		}
		Animation.Play("whip");
	}
}
