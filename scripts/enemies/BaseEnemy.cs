using Godot;

public partial class BaseEnemy : CharacterBody2D
{

    [Export]
    protected ItemDropper _itemDropper;
    protected CharacterBody2D _character;

    public override void _Ready()
    {
        CallDeferred(nameof(InitCharacter));
        CollisionLayer = 16;
        CollisionMask = 0 | 16;
        AddToGroup("enemy");
    }

    private void InitCharacter()
    {
        _character = this.GetCharacter();
    }

    public virtual void Die()
    {
        _itemDropper.DropItem();
        QueueFree();
    }

}
