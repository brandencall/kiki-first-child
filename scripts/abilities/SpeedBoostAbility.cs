using Godot;

public partial class SpeedBoostAbility : Ability
{
    [Export]
    public float SpeedMultiplier = 1.5f;

    public override void Apply(BasePlayer player)
    {
        player.VelocityComponent.MaxSpeed *= SpeedMultiplier;
        GD.Print(player.VelocityComponent.MaxSpeed);
    }
}   
