using Godot;

public partial class HurtboxComponent : Area2D
{
    [Export]
    private HealthComponent _healthComponent;

    public Node OwnerEntity { get; set; }

    public override void _Ready()
    {
        CollisionLayer = 32;
        CollisionMask = 2;
        Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
    }

    private void OnAreaEntered(Area2D otherArea)
    {
        if (otherArea is HitboxComponent hitbox)
        {
            DamageContext ctx = new DamageContext
            {
                Attacker = hitbox.OwnerEntity,
                Defender = this.OwnerEntity,
                BaseDamage = hitbox.Damage,
                FinalDamage = hitbox.Damage,
                Effects = hitbox.Effects
            };

            DamageManager.Resolve(ctx);
        }
    }

    public void DealDamage(float damage)
    {
        _healthComponent.Damage(damage);
    }
}
