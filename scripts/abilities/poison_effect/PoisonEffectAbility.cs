using System.Threading.Tasks;
using Godot;

public partial class PoisonEffectAbility : Node, IEffect
{
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public float Duration { get; set; } = 1.5f;
	[Export]
	public float TickInterval { get; set; } = 0.5f;

	public async Task Apply(BaseEnemy target)
	{
		float elapsed = 0f;

		while (elapsed < Duration)
		{
			target.HealthComponent.Damage(Damage);
			await ToSignal(GetTree().CreateTimer(TickInterval), "timeout");
			elapsed += TickInterval;
		}
	}
}
