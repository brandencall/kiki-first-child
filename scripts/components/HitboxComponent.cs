using Godot;

public partial class HitboxComponent : Area2D
{
    [Export]
    public float Damage { get; set; }

    public override void _Ready()
    { 
        CollisionMask = 32;
        CollisionLayer = 2;
    }
}
