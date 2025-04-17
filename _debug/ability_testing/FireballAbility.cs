using Godot;

public partial class FireballAbility : Ability
{
    [Export] public PackedScene FireballScene;

    public override void Apply(BasePlayer player)
    {
        var fireball = FireballScene.Instantiate<Node2D>();
        fireball.GlobalPosition = player.GlobalPosition;
        player.GetTree().Root.AddChild(fireball);
    }
}
