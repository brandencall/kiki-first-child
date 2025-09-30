using Godot;

public partial class HitboxComponent : Area2D
{
    [Export]
    public float Damage { get; set; }
    public PoisonEffectAbility PoisonEffectAbility { get; set; } = null;

    public override void _Ready()
    {
        CollisionMask = 32;
        CollisionLayer = 2;
    }
}
