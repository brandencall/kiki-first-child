using Godot;
using System;

public partial class SpeedAbility : Node
{
    public float speedIncrease = 100;
    public override void _Ready()
    {
        BasePlayer player = this.GetPlayer();
        player.VelocityComponent.MaxSpeed += 100;
        QueueFree();
    }
}
