using Godot;
using System.Threading.Tasks;

public partial class TooFastTooFuriousAbility : Node, IConditionalEffect
{
	// Modifies the entity movement state
	public bool IsStateModifier { get; } = false;
	[Export]
	public float DamageMultiplier { get; set; } = 0.05f;

	public void OnBeforeDealDamage(DamageContext ctx)
	{
		if (ctx.Defender is BaseEnemy enemy)
		{
			float speed = enemy.VelocityComponent.GetCurrentSpeed();
			ctx.FinalDamage += speed * DamageMultiplier;
		}
	}
}
