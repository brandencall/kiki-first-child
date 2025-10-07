using System.Threading.Tasks;
using Godot;

public partial class PoisonEffectAbility : Node, IEffect
{
	public bool IsStateModifier { get; } = false;
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public float Duration { get; set; } = 1.5f;
	[Export]
	public float TickInterval { get; set; } = 0.5f;

	public async Task Apply(DamageContext ctx)
	{
		// Change this from BaseEnemy to an Interface that the enemy implements
		if (ctx.Defender is BaseEnemy target)
		{
			float elapsed = 0f;
			target.ApplyPoison();

			while (elapsed < Duration)
			{
				DamageContext newCtx = CreateDamageContext(ctx);
				await ToSignal(GetTree().CreateTimer(TickInterval), "timeout");
				DamageManager.Resolve(newCtx);
				elapsed += TickInterval;
			}
			target.ClearPoison();
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
