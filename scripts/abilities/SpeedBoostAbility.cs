using Godot;

public partial class SpeedBoostAbility : AbilityLogic 
{
    [Export]
    public float SpeedIncrease = 2;

    public override void Apply(BasePlayer player)
    {
        player.VelocityComponent.MaxSpeed *= 2;
    }
}
