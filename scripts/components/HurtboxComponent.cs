using Godot;

public partial class HurtboxComponent : Area2D
{
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

			var effects = hitbox.Effects;
			// TODO: Should change this to some sort of IEntity so even the Characters can be effected
			BaseEnemy enemy = GetParent<BaseEnemy>();

			if (enemy != null)
			{
				foreach (var effect in effects)
				{
					if (effect.IsStateModifier)
					{
						//Fire and forget so that all effects are applied at the same time
						_ = effect.Apply(enemy);
					}
				}

				foreach (var effect in effects)
				{
					if (!effect.IsStateModifier)
					{
						//Fire and forget so that all effects are applied at the same time
						_ = effect.Apply(enemy);
					}
				}
			}
		}
	}

	private void DealDamage(float damage)
	{
		_healthComponent.Damage(damage);
		GD.Print("Damage: " + damage);
	}
}
