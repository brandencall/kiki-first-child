using Godot;

public partial class PurpleKnightPlayer : CharacterBody2D
{
    [Export]
    public AnimatedSprite2D Animations { get; set; }
    [Export]
    private HealthComponent _healthComponent;
    [Export]
    private VelocityComponent _velocityComponent;
    //TODO: May want to create a seperate "AttackComponent". This might help with implementing abiltities
    //and different weapons.
    [Export]
    private Area2D _hitboxComponent;
    [Export]
    private float _baseAttackCooldown = 1.5f;

    private CollisionShape2D _hitboxCollision;
    private bool _isAttacking = false;

    public override void _Ready()
    {
        CollisionLayer = 8;
        CollisionMask = 0;
        AddToGroup("player");
        _healthComponent.Died += Die;

        _hitboxCollision = _hitboxComponent.GetNode<CollisionShape2D>("CollisionShape2D");
        _hitboxCollision.Disabled = true;

        Timer baseAttackTimer = new Timer();
        AddChild(baseAttackTimer);
        baseAttackTimer.WaitTime += _baseAttackCooldown;
        baseAttackTimer.OneShot = false;
        baseAttackTimer.Timeout += OnBaseAttackTimerTimeout;
        baseAttackTimer.Start();
    }

    private void OnBaseAttackTimerTimeout()
    {
        _isAttacking = true;
        Animations.Play("attack");
        _hitboxCollision.Disabled = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        _velocityComponent.AccelerateInDirection(inputDirection);
        if (inputDirection.X != 0)
        {
            if (inputDirection.X < 0)
            {
                Animations.FlipH = true;
                _hitboxComponent.SetScale(new Vector2(-1, 1));
            }
            else
            {
                Animations.FlipH = false;
                _hitboxComponent.SetScale(new Vector2(1, 1));
            }
        }

        if (inputDirection.IsZeroApprox() && !_isAttacking)
        {
            _velocityComponent.Declerate();
            Animations.Play("idle");
        }
        else if (Velocity != Vector2.Zero && !_isAttacking)
        {
            Animations.Play("walk");
        }

        _velocityComponent.Move(this);
    }

    //TODO: Handle player dying
    private void Die()
    {
        GD.Print("Player has died");
    }

    public void OnAnimatedSprite2dAnimationFinished()
    {
        if (Animations.Animation == "attack")
        {
            _isAttacking = false;
            _hitboxCollision.Disabled = true;
        }
    }
}
