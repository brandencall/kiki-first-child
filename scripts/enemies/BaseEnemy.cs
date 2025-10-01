using Godot;

public partial class BaseEnemy : CharacterBody2D
{

    [Export]
    protected ItemDropper _itemDropper;
    [Export]
    public AnimatedSprite2D Animations { get; set; }
    [Export]
    public HealthComponent HealthComponent { get; set; }
    [Export]
    public VelocityComponent VelocityComponent { get; set; }
    [Export]
    public GpuParticles2D PoisonParticles { get; set; }
    [Export]
    public GpuParticles2D SlowParticles { get; set; }
    [Signal]
    public delegate void OnEnemyDiedEventHandler();
    protected CharacterBody2D _character;

    public override void _Ready()
    {
        CallDeferred(nameof(InitCharacter));
        CollisionLayer = 16;
        CollisionMask = 0 | 16;
        AddToGroup("enemy");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Velocity.X != 0)
        {
            if (Velocity.X < 0)
            {
                Animations.FlipH = true;
            }
            else
            {
                Animations.FlipH = false;
            }
        }
    }

    private void InitCharacter()
    {
        _character = this.GetCharacter();
    }

    public void ApplyPoison()
    {
        PoisonParticles.Emitting = true;
    }

    public void ClearPoison()
    {
        PoisonParticles.Emitting = false;
    }

    public void ApplySlow()
    {
        SlowParticles.Emitting = true;
    }

    public void ClearSlow()
    {
        SlowParticles.Emitting = false;
    }

    public virtual void Die()
    {
        _itemDropper.DropItem();
        EmitSignal(SignalName.OnEnemyDied);
        QueueFree();
    }

}
