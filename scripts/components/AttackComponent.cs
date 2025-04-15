using Godot;

public partial class AttackComponent : Node2D
{
    [Export]
    public Area2D AttackArea { get; set; }
    [Export]
    public float Cooldown { get; set; } = 1.5f;
    [Signal]
    public delegate void AttackEventHandler();

    private bool _isAttacking = false;

    public override void _Ready()
    {
        Timer attackTimer = new Timer();
        AddChild(attackTimer);
        attackTimer.WaitTime += Cooldown;
        attackTimer.OneShot = false;
        attackTimer.Timeout += OnAttackTimerTimeout;
        attackTimer.Start();
    }

    private void OnAttackTimerTimeout()
    {
        //Attack
        _isAttacking = true;
        EmitSignal(SignalName.Attack);
    }

    public void AttackFinished()
    {
        _isAttacking = false;
    }
}
