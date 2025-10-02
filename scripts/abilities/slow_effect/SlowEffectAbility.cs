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

	public async Task Apply(BaseEnemy target)
	{
		target.ApplySlow();
		target.VelocityComponent.AddSpeedMultiplier(SlowDownMultiplier);
		GD.Print("Speed updated in the SlowEffectAbility to newSpeed: " + target.VelocityComponent.GetCurrentSpeed());
		await ToSignal(GetTree().CreateTimer(Duration), "timeout");
		target.ClearSlow();
		target.VelocityComponent.RemoveSpeedMultiplier(SlowDownMultiplier);
		GD.Print("Speed updated in the SlowEffectAbility to baseSpeed: " + target.VelocityComponent.GetCurrentSpeed());
	}
}
