using Godot;
using System.Threading.Tasks;

public partial class SlowEffectAbility : Node, IEffect
{
	// Modifies the entity movement state
	public bool IsStateModifier { get; } = true;
	[Export]
	public float Duration { get; set; } = 1.5f;
	[Export]
	public float SlowDownMultiplier { get; set; } = 0.95f;

	public async Task Apply(DamageContext ctx)
	{
		// Change this from BaseEnemy to an Interface that the enemy implements
		if (ctx.Defender is BaseEnemy target)
		{
			target.ApplySlow();
			target.VelocityComponent.AddSpeedMultiplier(SlowDownMultiplier);
			await ToSignal(GetTree().CreateTimer(Duration), "timeout");
			target.ClearSlow();
			target.VelocityComponent.RemoveSpeedMultiplier(SlowDownMultiplier);
		}
	}
}
