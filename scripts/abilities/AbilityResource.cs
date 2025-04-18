using Godot;

[GlobalClass]
public partial class AbilityResource : Resource 
{
    [Export]
    public string AbilityName { get; set; }
    [Export]
    public string Description { get; set; }
    [Export]
    public Texture2D Icon { get; set; }
    //May need to add PackedScene for the actual ability.
    [Export]
    public PackedScene AbilityLogic { get; set; }
}
