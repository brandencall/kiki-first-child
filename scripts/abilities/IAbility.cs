using Godot;

public interface IAbility
{
    public string AbilityName { get; set; }
    public string Description { get; set; }
    public Texture2D AbilityIcon { get; set; }
    public int CurrentLevel { get; set; }
    public bool OnLastLevel { get; set; }
    public void Apply(BaseCharacter character);
    public void Upgrade();
}
