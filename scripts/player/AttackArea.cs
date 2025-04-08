using Godot;

public partial class AttackArea : Area2D
{
    public int? AttackDamage { get; set; } = null;

    public AttackArea()
    {
        CollisionLayer = 2;
        CollisionMask = 0;
    }
}
