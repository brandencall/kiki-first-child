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

			while (elapsed < Duration)
			{
				DamageContext newCtx = CreateDamageContext(ctx);
				await ToSignal(GetTree().CreateTimer(TickInterval), "timeout");
				DamageManager.Resolve(newCtx);
				elapsed += TickInterval;
			}
			target.ClearFire();
			target.VelocityComponent.RemoveSpeedMultiplier(SpeedUpMultiplier);
		}
	}

	private DamageContext CreateDamageContext(DamageContext ctx)
	{
		return new DamageContext
		{
			Attacker = ctx.Attacker,
			Defender = ctx.Defender,
			BaseDamage = Damage,
			FinalDamage = Damage,
		};

	}
}
