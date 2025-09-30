using System.Threading.Tasks;
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

	private async void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is HitboxComponent hitbox)
		{
			DealDamage(hitbox.Damage);

			// TODO: Need to update this to be a list of IEffects
			//var poisonAbility = hitbox.PoisonEffectAbility;
			var effects = hitbox.Effects;
			// TODO: Should change this to some sort of IEntity so even the Characters can be effected
			BaseEnemy enemy = GetParent<BaseEnemy>();

			if (enemy != null)
			{
				foreach (var effect in effects)
				{
					await effect.Apply(enemy);
				}
			}

			//if (poisonAbility != null && enemy != null)
			//{
			//    await poisonAbility.Apply(enemy);
			//}
		}
	}

	private void DealDamage(float damage)
	{
		_healthComponent.Damage(damage);
	}
}
