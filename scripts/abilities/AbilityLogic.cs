using Godot;

public abstract partial class AbilityLogic : Node
{
    public int CurrentLevel { get; set; } = 1;

    public virtual void Upgrade() => CurrentLevel++;
    public abstract void Apply(BasePlayer player);
}
