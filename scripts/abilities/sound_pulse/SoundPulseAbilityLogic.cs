using Godot;

public partial class SoundPulseAbilityLogic : AbilityLogic 
{
    [Export]
    public PackedScene SoundPulseScene { get; set; }
    private SoundPulseAbility _soundPulse;

    public override void Apply(BasePlayer player)
    {
        _soundPulse = SoundPulseScene.Instantiate<SoundPulseAbility>();
        player.AddChild(_soundPulse);
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
                _soundPulse.Damage += 5;
                break;
            case 3:
                _soundPulse.WaveSpeed += _soundPulse.WaveSpeed * .2f;
                break;
            case 4:
                _soundPulse.Damage += 5;
                _soundPulse.WaveSpeed += _soundPulse.WaveSpeed * .2f;
                break;
            case 5:
                _soundPulse.Damage += 10;
                break;
            case 6:
                _soundPulse.Damage += 10;
                break;
            case 7:
                _soundPulse.WaveSpeed += _soundPulse.WaveSpeed * .25f;
                break;
            case 8:
                _soundPulse.Damage += 15;
                _soundPulse.WaveSpeed += _soundPulse.WaveSpeed * .25f;
                OnLastLevel = true;
                break;
        }
    }

    private void SetupNextLevelAbilityResource(AbilityResource abilityResource)
    {
        switch (CurrentLevel)
        {
            case 2:
                abilityResource.Description = "+5 Damage increase!";
                break;
            case 3:
                abilityResource.Description = "20% Wave Speed increase!";
                break;
            case 4:
                abilityResource.Description = "+5 Damage increase and 20% Wave Speed increase";
                break;
            case 5:
                abilityResource.Description = "+10 Damage increase";
                break;
            case 6:
                abilityResource.Description = "+10 Damage increase";
                break;
            case 7:
                abilityResource.Description = "25% Wave Speed increase";
                break;
            case 8:
                abilityResource.Description = "+15 Damage increase and 25% Wave Speed increase";
                break;
        }
    }
}
