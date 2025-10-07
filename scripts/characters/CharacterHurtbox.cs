using Godot;

public partial class CharacterHurtbox : Area2D
{
    public Node OwnerEntity { get; set; }
    [Export]
    private HealthComponent _healthComponent;

    public override void _Ready()
    {
        CollisionLayer = 0;
        CollisionMask = 3;
        Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
    }

    private void OnAreaEntered(Area2D otherArea)
    {
        if (otherArea is EnemyHitbox hitbox)
        {
            //DealDamage(hitbox.Damage);
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
