using Godot;

public partial class BaseEnemy : CharacterBody2D
{

    [Export]
    protected ItemDropper _itemDropper;
    protected CharacterBody2D _player;

    public override void _Ready()
    {
        CallDeferred(nameof(InitPlayer));
        CollisionLayer = 16;
        CollisionMask = 0 | 16;
        AddToGroup("enemy");
    }

    private void InitPlayer()
    {
        _player = this.GetPlayer();
    }

    public virtual void Die()
    {
        _itemDropper.DropItem();
        QueueFree();
    }

}
