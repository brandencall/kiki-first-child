using Godot;
using System.Threading.Tasks;

public partial class TooFastTooFuriousAbility : Node, IEffect
{
	// Modifies the entity movement state
	public bool IsStateModifier { get; } = false;
	[Export]
	public float DamageMultiplier { get; set; } = 0.05f;

	public async Task Apply(BaseEnemy target)
	{
		float extraDamage = target.VelocityComponent.GetCurrentSpeed() * DamageMultiplier;
		GD.Print("Current Speed: " + target.VelocityComponent.GetCurrentSpeed());
		GD.Print("Extra Damage: " + extraDamage);
		target.HealthComponent.Damage(extraDamage);
	}
}
