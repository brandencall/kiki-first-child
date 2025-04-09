using Godot;

public partial class HitboxComponent : Area2D
{
    [Export]
    public float Damage { get; set; }

    public HitboxComponent()
    {
        CollisionLayer = 2;
        CollisionMask = 0;
    }
}
