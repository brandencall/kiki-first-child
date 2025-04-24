using Godot;

public abstract partial class AbilityLogic : Node
{
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel = false;

    public abstract void Apply(BasePlayer player);
    public virtual void Upgrade(AbilityResource abilityResource) => CurrentLevel++;
}
