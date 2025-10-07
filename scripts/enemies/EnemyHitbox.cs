using Godot;
using System.Collections.Generic;

public partial class EnemyHitbox : Area2D
{
    [Export]
    public float Damage { get; set; }

    public List<IEffect> Effects { get; set; } = new();
    public Node OwnerEntity { get; set; }

    public EnemyHitbox()
    {
        CollisionLayer = 3;
        CollisionMask = 0;
    }
}
