using Godot;

public class Football : IAbility
{
    public string AbilityName { get; set; } = "Football Ability";
    public string Description { get; set; } = "Throw a football in the direction you are facing";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/abilities/FootballIcon.png");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private PackedScene _footballScene = ResourceLoader.Load<PackedScene>("res://scenes/abilities/football/football_ability.tscn"); 
    private FootballAbility _football;

    public void Apply(BasePlayer player)
    {
        _football = _footballScene.Instantiate<FootballAbility>();
        player.GetTree().Root.AddChild(_football);
    }

    public void Upgrade()
    {
        CurrentLevel++;
    }
}
