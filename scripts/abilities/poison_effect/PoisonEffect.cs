using Godot;

public class PoisonEffect : IAbility
{
    public string AbilityName { get; set; } = "PoisonEffect";
    public string Description { get; set; } = "Apply Poison Effect on hit!";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/icon.svg");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private PackedScene _poisonEffectScene = ResourceLoader.Load<PackedScene>("res://scenes/abilities/poison_effect/poison_effect_ability.tscn");
    private PoisonEffectAbility _poisonEffect;

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
                _poisonEffect = _poisonEffectScene.Instantiate<PoisonEffectAbility>();
                character.AddChild(_poisonEffect);
                character.CallDeferred(nameof(character.AddPoisonEffect), _poisonEffect);
                break;
            case 2:
                _poisonEffect.Damage += 1;
                break;
            case 3:
                _poisonEffect.Damage += 1;
                break;
            case 4:
                _poisonEffect.Damage += 1;
                break;
            case 5:
                _poisonEffect.Damage += 1;
                break;
            case 6:
                _poisonEffect.Damage += 1;
                break;
            case 7:
                _poisonEffect.Damage += 1;
                break;
            case 8:
                _poisonEffect.Damage += 1;
                break;
        }
    }

    private void SetupNextLevelAbilityResource()
    {
        switch (CurrentLevel)
        {
            case 2:
                Description = "+1 dmg";
                break;
            case 3:
                Description = "+1 dmg";
                break;
            case 4:
                Description = "+1 dmg";
                break;
            case 5:
                Description = "+1 dmg";
                break;
            case 6:
                Description = "+1 dmg";
                break;
            case 7:
                Description = "+1 dmg";
                break;
            case 8:
                Description = "+1 dmg";
                break;
        }
    }
}
