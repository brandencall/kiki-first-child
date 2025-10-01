using Godot;

public partial class Chavz : BaseCharacter
{
    [Export]
    public Steak Steak { get; set; }

    public override void _Ready()
    {
        base._Ready();
        BaseAttackDamage = 20;
        Steak.Initialize(this, BaseAttackDamage);
    }

    public override void UpdateBaseAttackDamage(float newDamage)
    {
        base.UpdateBaseAttackDamage(newDamage);
        Steak.Damage = newDamage;
    }

    public override void OnBaseAttackTimerTimeout()
    {
        base.OnBaseAttackTimerTimeout();
        Steak.Effects = Effects;
        Steak.Attack();
    }
}
