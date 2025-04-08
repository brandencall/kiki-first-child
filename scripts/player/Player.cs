using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public BasePlayerStats Stats { get; set; }
    [Export]
    public AnimatedSprite2D Animations { get; set; }
    [Export]
    public AttackArea AttackArea { get; set; }
    [Export]
    private Camera2D _camera;
    [Signal]
    public delegate void PlayerMovedEventHandler(Vector2 newPosition);
    [Signal]
    public delegate void PlayerDiedEventHandler();

    private CollisionShape2D attackCollision { get; set; }
    private bool _isAttacking = false;

    public override void _Ready()
    {
        AddToGroup("players");
        attackCollision = AttackArea.GetNode<CollisionShape2D>("AttackCollision");
        AttackArea.AttackDamage = Stats.Damage;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Velocity = inputDirection * Stats.Speed;

        if (inputDirection.X != 0)
        {
            if (inputDirection.X < 0)
            {
                Animations.FlipH = true;
                AttackArea.SetScale(new Vector2(-1,1));
            }
            else 
            {
                Animations.FlipH = false;
                AttackArea.SetScale(new Vector2(1,1));
            }
        }

        if (Velocity == Vector2.Zero && !_isAttacking)
        {
            Animations.Play("idle");
        }
        else if (Velocity != Vector2.Zero && !_isAttacking)
        {
            Animations.Play("walk");
        }
        MoveAndSlide();
        EmitSignal(SignalName.PlayerMoved, GlobalPosition);
    }

    public void TakeDamage(int damage)
    {
        Stats.Health -= damage;

        if (Stats.Health <= 0)
        {
            Die();
        }
    }

    public Vector2 GetCameraBounds()
    {
        var screenSize = GetViewportRect().Size;
        Vector2 zoomedSize = screenSize / _camera.Zoom;
        Vector2 topLeft = _camera.GlobalPosition - zoomedSize / 2;
        return topLeft;
    }

    public void OnBaseAttackCooldownTimeout()
    {
        Attack();
    }

    public void Attack()
    {
        _isAttacking = true;
        Animations.Play("attack");
        attackCollision.Disabled = false;
    }

    public void OnAnimatedSprite2dAnimationFinished()
    {
        if (Animations.Animation == "attack")
        {
            _isAttacking = false;
            attackCollision.Disabled = true;
        }
    }

    private void Die()
    {
        GD.Print("Player died");
        EmitSignal(SignalName.PlayerDied);
    }
}
