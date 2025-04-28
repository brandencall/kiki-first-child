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
        UpgradeBasedOnLevel();
        CurrentLevel++;
        SetupNextLevelAbilityResource();
    }

    private void UpgradeBasedOnLevel()
    {
        switch (CurrentLevel)
        {
            case 2:
                _football.MaxNumberOfEnemiesHit = 2;
                break;
            case 3:
                _football.Damage += 10;
                _football.Cooldown.WaitTime -= _football.Cooldown.WaitTime * .2; 
                break;
            case 4:
                _football.MaxNumberOfEnemiesHit = 4;
                _football.Damage += 10;
                break;
            case 5:
                _football.Damage += 15;
                break;
            case 6:
                _football.MaxNumberOfEnemiesHit = 6;
                _football.Cooldown.WaitTime -= _football.Cooldown.WaitTime * .2; 
                break;
            case 7:
                _football.Damage += 20;
                break;
            case 8:
                _football.MaxNumberOfEnemiesHit = 10;
                _football.Damage += 25;
                _football.Cooldown.WaitTime -= _football.Cooldown.WaitTime * .25; 
                break;
        }
    }

    private void SetupNextLevelAbilityResource()
    {
        switch (CurrentLevel)
        {
            case 2:
                Description = "Football can now smack 2 enemies!";
                break;
            case 3:
                Description = "+10 Damage and throw it 20% more often!";
                break;
            case 4:
                Description = "Football can now smack 4 enemies and +10 Damage!";
                break;
            case 5:
                Description = "+15 Damage!";
                break;
            case 6:
                Description = "Football can now smack 6 enemies and throw it 20% more often!";
                break;
            case 7:
                Description = "+20 Damage!";
                break;
            case 8:
                Description = "Football can now smack 10 enemies, +25 Damage and throw it 25% more often!";
                break;
        }
    }
}
