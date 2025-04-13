using Godot;
using System;

public partial class BaseEnemy : CharacterBody2D
{

    protected CharacterBody2D _player;

    public override void _Ready()
    {
        CollisionLayer = 16;
        CollisionMask = 0 | 16;
        AddToGroup("enemy");
        _player = this.GetPlayer();
    }

}
