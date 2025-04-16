using Godot;

public partial class BasePlayer : CharacterBody2D
{
    [Export]
    public AnimatedSprite2D Animations { get; set; }
    [Export]
    public VelocityComponent VelocityComponent { get; set; }
    [Export]
    private HealthComponent _healthComponent;

    //TODO: May want to create a seperate "AttackComponent". This might help with implementing abiltities
    //and different weapons.
    [Export]
    private AttackComponent _attackComponent;
    [Export]
    private Area2D _hitboxComponent;
    [Export]
    private float _baseAttackCooldown = 1.5f;

    private CollisionShape2D _hitboxCollision;
    private bool _isAttacking = false;

    [Signal]
    public delegate void ExperiencePickedupEventHandler(float experience);

    public override void _Ready()
    {
        CollisionLayer = 8;
        CollisionMask = 0;
        AddToGroup("player");
        _healthComponent.Died += Die;
        GetNode<Area2D>("PickupArea").AreaEntered += OnPickupAreaEntered;

        _hitboxCollision = _hitboxComponent.GetNode<CollisionShape2D>("CollisionShape2D");
        _hitboxCollision.Disabled = true;

        Timer baseAttackTimer = new Timer();
        AddChild(baseAttackTimer);
        baseAttackTimer.WaitTime += _baseAttackCooldown;
        baseAttackTimer.OneShot = false;
        baseAttackTimer.Timeout += OnBaseAttackTimerTimeout;
        baseAttackTimer.Start();

        GodotUtilities.RegisterPlayer(this);
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
        VelocityComponent.AccelerateInDirection(inputDirection);
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
            VelocityComponent.Declerate();
            Animations.Play("idle");
        }
        else if (Velocity != Vector2.Zero && !_isAttacking)
        {
            Animations.Play("walk");
        }

        VelocityComponent.Move(this);
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

    //TODO: add some pickup logic
    private void OnPickupAreaEntered(Area2D area)
    {
        if (area is IExperience item)
        {
            EmitSignal(SignalName.ExperiencePickedup, item.Experience);
            item.QueueFree();
        }
    }
}
