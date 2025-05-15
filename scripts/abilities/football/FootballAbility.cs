using Godot;

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
    private BaseCharacter _character;
    private int _currentEnemiesHit = 0;
    private float _currentSpeed;

    public override void _Ready()
    {
        base._Ready();
        _character = this.GetCharacter();
        GlobalPosition = _character.GlobalPosition;
        _direction = _character.VelocityComponent.Velocity.Normalized();
        Sprite.Rotation = _direction.Angle();
        Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
        Cooldown.Timeout += ResetFootball;
        this.AreaEntered += OnAreaEntered;
        _currentSpeed = Speed;
    }

    private void OnAreaEntered(Area2D otherArea)
    {
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
        GlobalPosition = _character.GlobalPosition;
        _direction = _character.VelocityComponent.Velocity.Normalized();
        Sprite.Rotation = _direction.Angle();
        Collision.Rotation = _direction.Angle() - Mathf.Pi / 2f;
    }

}
