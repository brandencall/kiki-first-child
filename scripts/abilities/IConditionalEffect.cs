using Godot;
using System;

public interface IConditionalEffect
{
    void OnBeforeDealDamage(DamageContext ctx);
}
