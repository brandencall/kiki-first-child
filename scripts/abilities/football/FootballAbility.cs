using Godot;
using System;

public partial class FootballAbility : HitboxComponent
{
    [Export]
    public Sprite2D Sprite { get; set; }
    [Export] 
    public CollisionShape2D Collision { get; set; }
    [Export]
    public Timer Cooldown { get; set; }
    [Export] 
    public float Speed = 400f;

    private Vector2 _direction; 
    private BasePlayer _player;

    public override void _Ready()
    {
        base._Ready();
        _player = this.GetPlayer();
        GlobalPosition = _player.GlobalPosition;
        _direction = _player.VelocityComponent.Velocity.Normalized();
        Sprite.Rotation = _direction.Angle();
        Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
        Cooldown.Timeout += ResetFootball;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += _direction.Normalized() * Speed * (float)delta;
    }

    private void ResetFootball()
    {
        GlobalPosition = _player.GlobalPosition;
        _direction = _player.VelocityComponent.Velocity.Normalized();
        Sprite.Rotation = _direction.Angle();
        Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
    }

}
