
public partial class SpeedBoostAbility : AbilityLogic
{
    private BasePlayer _player;

    public override void Apply(BasePlayer player)
    {
        _player = player;
        player.VelocityComponent.MaxSpeed += player.VelocityComponent.MaxSpeed * 0.2f;
    }

    public override void Upgrade(AbilityResource abilityResource)
    {
        UpgradeBasedOnLevel();
        base.Upgrade(abilityResource);
        SetupNextLevelAbilityResource(abilityResource);
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

    private void SetupNextLevelAbilityResource(AbilityResource abilityResource)
    {
        switch (CurrentLevel)
        {
            case 2:
                abilityResource.Description = "+20% speed increase!";
                break;
            case 3:
                abilityResource.Description = "+20% speed increase!";
                break;
            case 4:
                abilityResource.Description = "+20% speed increase!";
                break;
            case 5:
                abilityResource.Description = "+20% speed increase!";
                break;
            case 6:
                abilityResource.Description = "+20% speed increase!";
                break;
            case 7:
                abilityResource.Description = "+20% speed increase!";
                break;
            case 8:
                abilityResource.Description = "+20% speed increase!";
                break;
        }
    }
}
