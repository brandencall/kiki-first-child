using Godot;

public partial class Hodge : BaseCharacter
{
    [Export]
    public Noodle Noodle { get; set; }

    public override void _Ready()
    {
        base._Ready();
        BaseAttackDamage = 10f;
        Noodle.Initialize(this, BaseAttackDamage);
        Noodle.OwnerEntity = this;
    }

    public override void UpdateBaseAttackDamage(float newDamage)
    {
        base.UpdateBaseAttackDamage(newDamage);
        Noodle.Damage = newDamage;
    }

    // TODO: Make it so the effects is only applied to weapon once when the effect is initially added
    // THIS NEEDS TO BE DONE FOR ALL CHARACTERS
    public override void OnBaseAttackTimerTimeout()
    {
        base.OnBaseAttackTimerTimeout();
        Noodle.Effects = Effects;
        Noodle.Attack();
    }
}
