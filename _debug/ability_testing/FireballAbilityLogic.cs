using Godot;

public partial class FireballAbilityLogic : AbilityLogic
{
    [Export]
    public PackedScene Fireball { get; set; }

    public override void Apply(BasePlayer player)
    {
        var fireball = Fireball.Instantiate<Node2D>();
        fireball.GlobalPosition = player.GlobalPosition;
        player.GetTree().Root.AddChild(fireball);
    }   
}
