using Godot;

public partial class Noodle : HitboxComponent
{
	[Export]
	public AnimationPlayer Animation { get; set; }
	[Export]
	public Sprite2D Sprite { get; set; }

	private BaseCharacter _character;

	public void Initialize(BaseCharacter character, float damage)
	{
		_character = character;
        Damage = damage;
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

	public void AttackBackwards()
	{
		Rotation = _character.VelocityComponent.LastMoveDirection.Angle() + Mathf.Pi;
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
