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
    public float Speed = 700f;
    [Export]
    public int MaxNumberOfEnemiesHit { get; set; } = 1;

    private Vector2 _direction;
    private BasePlayer _player;
    private int _currentEnemiesHit = 0;
    private float _currentSpeed;

    public override void _Ready()
    {
        base._Ready();
        _player = this.GetPlayer();
        GlobalPosition = _player.GlobalPosition;
        _direction = _player.VelocityComponent.Velocity.Normalized();
        Sprite.Rotation = _direction.Angle();
        Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
        Cooldown.Timeout += ResetFootball;
        this.AreaEntered += OnAreaEntered;
        _currentSpeed = Speed;
    }

    private void OnAreaEntered(Area2D otherArea)
    {
        GD.Print("area_entered");
        if (otherArea is HurtboxComponent hurtbox)
        {
            _currentEnemiesHit++;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += _direction.Normalized() * _currentSpeed * (float)delta;
        CheckEnemiesHit();
    }

    private void CheckEnemiesHit()
    {
        if (_currentEnemiesHit >= MaxNumberOfEnemiesHit)
        {
            Collision.Disabled = true;
            Sprite.Visible = false;
            _currentSpeed = 0;
        }
    }

    private void ResetFootball()
    {
        Collision.Disabled = false;
        Sprite.Visible = true;
        _currentSpeed = Speed;
        _currentEnemiesHit = 0;
        GlobalPosition = _player.GlobalPosition;
        _direction = _player.VelocityComponent.Velocity.Normalized();
        Sprite.Rotation = _direction.Angle();
        Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
    }

}
