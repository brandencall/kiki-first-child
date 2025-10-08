using Godot;
using System.Collections.Generic;

public class DamageContext
{
    public Node Attacker;
    public Node Defender;
    public float BaseDamage;
    public float FinalDamage;
    public List<IEffect> Effects = new List<IEffect>();
}

public static class DamageManager
{
    public static void Resolve(DamageContext ctx)
    {
        foreach (var effect in ctx.Effects)
        {
            _ = effect.Apply(ctx);
        }

        if (ctx.Attacker is IHasConditionalEffects attackerEffects)
        {
            foreach (var effect in attackerEffects.GetConditionalEffects())
            {
                effect.OnBeforeDealDamage(ctx);
            }
        }

        if (ctx.Defender is IHasHealth health)
        {
            health.ApplyDamage(ctx.FinalDamage);
        }

    }
}
