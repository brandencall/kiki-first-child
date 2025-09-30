using Godot;

public class SoundPulse : IAbility
{
    public string AbilityName { get; set; } = "Sound Pulse Ability";
    public string Description { get; set; } = "Distribute a sound wave around the player";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/abilities/SoundPulseIcon.png");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private PackedScene _soundPulseScene = ResourceLoader.Load<PackedScene>("res://scenes/abilities/sound_pulse/sound_pulse_ability.tscn");
    private SoundPulseAbility _soundPulse;

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
                _soundPulse = _soundPulseScene.Instantiate<SoundPulseAbility>();
                character.AddChild(_soundPulse);
                break;
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

    private void SetupNextLevelAbilityResource()
    {
        switch (CurrentLevel)
        {
            case 2:
                Description = "+5 Damage increase!";
                break;
            case 3:
                Description = "20% Wave Speed increase!";
                break;
            case 4:
                Description = "+5 Damage increase and 20% Wave Speed increase";
                break;
            case 5:
                Description = "+10 Damage increase";
                break;
            case 6:
                Description = "+10 Damage increase";
                break;
            case 7:
                Description = "25% Wave Speed increase";
                break;
            case 8:
                Description = "+15 Damage increase and 25% Wave Speed increase";
                break;
        }
    }
}
