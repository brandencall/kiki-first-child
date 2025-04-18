using Godot;
using System;

public partial class Fireball : Node2D
{
    [Export] public float Speed = 400f;
    [Export] public Vector2 Direction = Vector2.Right;

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction.Normalized() * Speed * (float)delta;
    }

    private void OnAreaEntered(Area2D area)
    {
        GD.Print("Fireball hit: ", area.Name);
    }

    public override void _Ready()
    {
        BasePlayer player = this.GetPlayer();
        GlobalPosition = player.GlobalPosition;
        GetNode<Area2D>("Area2D").AreaEntered += OnAreaEntered;
    }
}
