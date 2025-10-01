using Godot;
using System.Collections.Generic;

public partial class BaseCharacter : CharacterBody2D
{

    [Export]
    public AnimatedSprite2D Animations { get; set; }
    [Export]
    public VelocityComponent VelocityComponent { get; set; }
    [Export]
    public HealthComponent HealthComponent { get; set; }
    [Export]
    public float BaseAttackCooldown = 1.5f;
    [Export]
    public float BaseAttackDamage = 10f;

    public SkillTree SkillTree { get; set; }
    public CharacterData CharacterData { get; private set; }

    public List<IEffect> Effects { get; set; } = new();

    private CollisionShape2D _hitboxCollision;
    private bool _isAttacking = false;
    private Timer _baseAttackTimer;

    [Signal]
    public delegate void ExperiencePickedupEventHandler(float experience);
    [Signal]
    public delegate void OnCharacterDiedEventHandler();
    [Signal]
    public delegate void AttackEventHandler();

    public override void _Ready()
    {
        CollisionLayer = 8;
        CollisionMask = 0;
        AddToGroup("character");
        HealthComponent.Died += Die;
        GetNode<Area2D>("PickupArea").AreaEntered += OnPickupAreaEntered;

        _baseAttackTimer = new Timer();
        AddChild(_baseAttackTimer);
        _baseAttackTimer.WaitTime = BaseAttackCooldown;
        _baseAttackTimer.OneShot = false;
        _baseAttackTimer.Timeout += OnBaseAttackTimerTimeout;
        _baseAttackTimer.Start();

        //await ToSignal(GetTree().CreateTimer(TickInterval), "timeout");
        GodotUtilities.RegisterCharacter(this);
    }

    public void Initialize(CharacterData characterData)
    {
        CharacterData = characterData;
        CreateAndApplySkillTree();
    }

    public void CreateAndApplySkillTree()
    {
        SkillTree = new SkillTree(CharacterData.Skills, this);
    }

    public void UpdateAttackCooldown(float newCooldown)
    {
        BaseAttackCooldown = newCooldown;
        _baseAttackTimer.WaitTime = BaseAttackCooldown;

        if (_baseAttackTimer.TimeLeft > newCooldown)
        {
            _baseAttackTimer.Start();
        }
    }

    public virtual void UpdateBaseAttackDamage(float newDamage)
    {
        BaseAttackDamage = newDamage;
    }

    public virtual void OnBaseAttackTimerTimeout()
    {
        EmitSignal(SignalName.Attack);
        _isAttacking = true;
        Animations.Play("attack");
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
            }
            else
            {
                Animations.FlipH = false;
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
        EmitSignal(SignalName.OnCharacterDied);
    }

    public void OnAnimatedSprite2dAnimationFinished()
    {
        if (Animations.Animation == "attack")
        {
            _isAttacking = false;
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
