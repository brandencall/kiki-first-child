using Godot;
using System;

public partial class EnemyHitbox : Area2D
{
    [Export]
    public float Damage { get; set; }

    public EnemyHitbox()
    {
        CollisionLayer = 3;
        CollisionMask = 0;
    }
}
