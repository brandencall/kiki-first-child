using Godot;

public class SpeedBoost : IAbility
{
    public string AbilityName { get; set; } = "Speed Boost";
    public string Description { get; set; } = "20% speed increase!";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/abilities/SpeedBoostIcon.png");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    public void Upgrade(BaseCharacter character)
    {
        UpgradeBasedOnLevel(character);
        CurrentLevel++;
        SetupNextLevelAbilityResource();
    }

    private void UpgradeBasedOnLevel(BaseCharacter character)
    {
        switch (CurrentLevel)
        {
            case 1:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 2:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 3:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 4:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 5:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 6:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 7:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
                break;
            case 8:
                character.VelocityComponent.BaseSpeed += character.VelocityComponent.BaseSpeed * 0.2f;
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
