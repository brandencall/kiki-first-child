using Godot;

public partial class SlowEffect : IAbility
{
    public string AbilityName { get; set; } = "SlowEffect";
    public string Description { get; set; } = "Slows the enemy!";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/icon.svg");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private PackedScene _slowEffectScene = ResourceLoader.Load<PackedScene>("res://scenes/abilities/slow_effect/slow_effect_ability.tscn");
    private SlowEffectAbility _slowEffect;

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
                _slowEffect = _slowEffectScene.Instantiate<SlowEffectAbility>();
                character.AddChild(_slowEffect);
                character.Effects.Add(_slowEffect);
                break;
            case 2:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
            case 3:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
            case 4:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
            case 5:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
            case 6:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
            case 7:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
            case 8:
                _slowEffect.SlowDownMultiplier -= 0.05f;
                break;
        }
    }

    private void SetupNextLevelAbilityResource()
    {
        switch (CurrentLevel)
        {
            case 2:
                Description = "Slow the enemies 5% more";
                break;
            case 3:
                Description = "Slow the enemies 5% more";
                break;
            case 4:
                Description = "Slow the enemies 5% more";
                break;
            case 5:
                Description = "Slow the enemies 5% more";
                break;
            case 6:
                Description = "Slow the enemies 5% more";
                break;
            case 7:
                Description = "Slow the enemies 5% more";
                break;
            case 8:
                Description = "Slow the enemies 5% more";
                break;
        }
    }
}
