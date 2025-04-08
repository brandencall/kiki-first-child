using Godot;

public partial class Hitbox : Area2D
{
    [Signal]
    public delegate void TakeDamageEventHandler(int attackDamage);

    public Hitbox()
    {
        CollisionLayer = 0;
        CollisionMask = 2;
    }

    public override void _Ready()
    {
    }

    public void OnHitboxAreaEntered(AttackArea attackArea)
    {
        GD.Print("here");
        if (attackArea == null)
        {
            return;
        }
        else if (attackArea.AttackDamage == null)
        {
            GD.Print("No AttackDamage set on AttackArea");
            return;
        }

        EmitSignal(SignalName.TakeDamage, (int)attackArea.AttackDamage);
    }
}
