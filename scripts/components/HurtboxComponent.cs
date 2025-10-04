using Godot;

public partial class HurtboxComponent : Area2D
{
	[Export]
	private HealthComponent _healthComponent;

	public Node OwnerEntity { get; set; }

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
			GD.Print("HurtboxComponent Owner: " + OwnerEntity);
			GD.Print("HitboxComponent Owner: " + hitbox.OwnerEntity);

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
	}
}
