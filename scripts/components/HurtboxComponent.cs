using Godot;

public partial class HurtboxComponent : Area2D
{
	[Signal]
	public delegate void HitByHitboxEventHandler(HitboxComponent hitboxComponent);

	[Export]
	private HealthComponent _healthComponent;

	public override void _Ready()
	{
		CollisionLayer = 32;
		CollisionMask = 2;
		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
	}

	private void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is HitboxComponent hitbox)
		{
			DealDamage(hitbox.Damage);
			EmitSignal(SignalName.HitByHitbox, hitbox);
		}
	}

	private void DealDamage(float damage)
	{
		_healthComponent.Damage(damage);
	}
}
