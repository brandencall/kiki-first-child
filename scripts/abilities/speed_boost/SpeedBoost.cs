using Godot;

public class SpeedBoost : IAbility
{
    public string AbilityName { get; set; } = "Speed Boost";
    public string Description { get; set; } = "20% speed increase!";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/SpeedBoostIcon.png");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private BasePlayer _player;

    public void Apply(BasePlayer player)
    {
        _player = player;
        player.VelocityComponent.MaxSpeed += player.VelocityComponent.MaxSpeed * 0.2f;
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
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 3:
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 4:
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 5:
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 6:
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 7:
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 8:
                _player.VelocityComponent.MaxSpeed += _player.VelocityComponent.MaxSpeed * 0.2f;
                break;
        }
    }

    private void SetupNextLevelAbilityResource()
    {
        switch (CurrentLevel)
        {
            case 2:
                Description = "+20% speed increase!";
                break;
            case 3:
                Description = "+20% speed increase!";
                break;
            case 4:
                Description = "+20% speed increase!";
                break;
            case 5:
                Description = "+20% speed increase!";
                break;
            case 6:
                Description = "+20% speed increase!";
                break;
            case 7:
                Description = "+20% speed increase!";
                break;
            case 8:
                Description = "+20% speed increase!";
                break;
        }
    }

}
