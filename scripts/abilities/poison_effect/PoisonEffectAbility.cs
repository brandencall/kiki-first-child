using System.Threading.Tasks;
using Godot;

public partial class PoisonEffectAbility : Node
{
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public float Duration { get; set; } = 1.5f;
	[Export]
	public float TickInterval { get; set; } = 0.5f;

	public async Task Apply(BaseEnemy target)
	{
		GD.Print("Applying poison dmg. Health before: " + target.HealthComponent.CurrentHealth);

		float elapsed = 0f;

		while (elapsed < Duration)
		{
			GD.Print("Ticking...");
			target.HealthComponent.Damage(Damage);
			await ToSignal(GetTree().CreateTimer(TickInterval), "timeout");
			elapsed += TickInterval;
		}
		GD.Print("Damage after: " + target.HealthComponent.CurrentHealth);

	}
}
