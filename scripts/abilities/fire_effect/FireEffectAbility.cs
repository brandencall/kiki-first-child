using Godot;
using System.Threading.Tasks;

public partial class FireEffectAbility : Node, IEffect
{
	// Modifies the entity movement state
	public bool IsStateModifier { get; } = true;
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public float Duration { get; set; } = 1.5f;
	[Export]
	public float TickInterval { get; set; } = 0.5f;
	[Export]
	public float SpeedUpMultiplier = 1.05f;

	public async Task Apply(DamageContext ctx)
	{
		// Change this from BaseEnemy to an Interface that the enemy implements
		if (ctx.Defender is BaseEnemy target)
		{
			target.VelocityComponent.AddSpeedMultiplier(SpeedUpMultiplier);
			float elapsed = 0f;
			target.ApplyFire();

			while (elapsed <= Duration)
			{
				await ToSignal(GetTree().CreateTimer(TickInterval), "timeout");
				target.HealthComponent.Damage(Damage);
				elapsed += TickInterval;
			}
			target.ClearFire();
			target.VelocityComponent.RemoveSpeedMultiplier(SpeedUpMultiplier);
		}
	}
}
