using Godot;

public partial class PlayerHurtbox : Area2D
{
	[Export]
	private HealthComponent _healthComponent;

	public PlayerHurtbox()
	{
		CollisionLayer = 0;
		CollisionMask = 3;
	}

	public override void _Ready()
	{
		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
	}

	private void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is EnemyHitbox hitbox)
		{
			DealDamage(hitbox.Damage);
		}
	}

	private void DealDamage(float damage)
	{
		_healthComponent.Damage(damage);
	}
}
