using Godot;

public partial class SoundPulseAbilityLogic : AbilityLogic 
{
    [Export]
    public PackedScene SoundPulse { get; set; }

    public override void Apply(BasePlayer player)
    {
        var soundPulse = SoundPulse.Instantiate<HitboxComponent>();
        player.AddChild(soundPulse);
    }   
}
