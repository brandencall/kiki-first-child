using Godot;

public class SpeedBoost : IAbility
{
    public string AbilityName { get; set; } = "Speed Boost";
    public string Description { get; set; } = "20% speed increase!";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/abilities/SpeedBoostIcon.png");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private BaseCharacter _character;

    public void Apply(BaseCharacter character)
    {
        _character = character;
        character.VelocityComponent.MaxSpeed += character.VelocityComponent.MaxSpeed * 0.2f;
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
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 3:
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 4:
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 5:
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 6:
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 7:
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
                break;
            case 8:
                _character.VelocityComponent.MaxSpeed += _character.VelocityComponent.MaxSpeed * 0.2f;
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
