using Godot;

public partial class FireEffect : IAbility
{
    public string AbilityName { get; set; } = "FireEffect";
    public string Description { get; set; } = "Apply fire damage to enemies but they move faster!";
    public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/icon.svg");
    public int CurrentLevel { get; set; } = 1;
    public bool OnLastLevel { get; set; } = false;

    private PackedScene _fireEffectScene = ResourceLoader.Load<PackedScene>("res://scenes/abilities/fire_effect/fire_effect_ability.tscn");
    private FireEffectAbility _fireEffect;

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
                _fireEffect = _fireEffectScene.Instantiate<FireEffectAbility>();
                character.AddChild(_fireEffect);
                character.Effects.Add(_fireEffect);
                break;
            case 2:
                _fireEffect.Damage += 1;
                break;
            case 3:
                _fireEffect.Damage += 1;
                break;
            case 4:
                _fireEffect.Damage += 1;
                break;
            case 5:
                _fireEffect.Damage += 1;
                break;
            case 6:
                _fireEffect.Damage += 1;
                break;
            case 7:
                _fireEffect.Damage += 1;
                break;
            case 8:
                _fireEffect.Damage += 1;
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
