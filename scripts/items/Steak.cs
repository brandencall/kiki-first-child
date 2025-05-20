using Godot;
using System;

public partial class Steak : HitboxComponent
{
	[Export]
	public AnimationPlayer Animation { get; set; }
	[Export]
	public Sprite2D Sprite { get; set; }
	[Export]
	public CollisionShape2D Collision { get; set; }

	private BaseCharacter _character;

	public override void _Ready()
	{
		base._Ready();
		this.Visible = false;
		Collision.Disabled = true;
		Animation.AnimationFinished += OnAnimationFinished;
	}

	public void Initialize(BaseCharacter character)
	{
		_character = character;
	}

	public void Attack()
	{
		if (_character.VelocityComponent.LastMoveDirection.X < 0)
		{
			Animation.Play("steak_backward");
		}
		else
		{
			Animation.Play("steak_forward");
		}
		this.Visible = true;
		Collision.Disabled = false;
	}

	public void OnAnimationFinished(StringName animationName)
	{
		if (animationName == "steak_forward" || animationName == "steak_backward")
		{
			this.Visible = false;
			Collision.Disabled = true;
		}
	}
}
