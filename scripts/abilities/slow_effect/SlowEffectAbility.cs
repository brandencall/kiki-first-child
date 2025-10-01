using Godot;
using System.Threading.Tasks;

public partial class SlowEffectAbility : Node, IEffect
{
	public int Damage { get; set; } = 1;
	[Export]
	public float Duration { get; set; } = 1.5f;
	[Export]
	public float SlowPercentage { get; set; } = 0.05f;

	public async Task Apply(BaseEnemy target)
	{
		float baseSpeed = target.VelocityComponent.MaxSpeed;
		float newSpeed = baseSpeed - (baseSpeed * SlowPercentage);
		target.VelocityComponent.MaxSpeed = newSpeed;
		target.ApplySlow();
		GD.Print("Velocity while slow: " + target.VelocityComponent.MaxSpeed);
		await ToSignal(GetTree().CreateTimer(Duration), "timeout");
		target.VelocityComponent.MaxSpeed = baseSpeed;
		target.ClearSlow();
		GD.Print("Velocity after slow: " + target.VelocityComponent.MaxSpeed);
	}
}
