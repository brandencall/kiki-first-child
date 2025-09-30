using Godot;
using System.Collections.Generic;

public partial class HitboxComponent : Area2D
{
    [Export]
    public float Damage { get; set; }
    //public PoisonEffectAbility PoisonEffectAbility { get; set; } = null;
    public List<IEffect> Effects { get; set; }

    public override void _Ready()
    {
        CollisionMask = 32;
        CollisionLayer = 2;
    }
}
