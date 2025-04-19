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
    [Export]
    public PackedScene AbilityLogicScene { get; set; }

    public AbilityLogic AbilityLogic { get; private set; }

    public void InitializeAbilityLogic()
    {
        if (AbilityLogicScene != null)
        {
            AbilityLogic = AbilityLogicScene.Instantiate<AbilityLogic>();
        }
    }
}
