using Godot;

public partial class BaseAttackUpgrade : IAbility
{
    public string AbilityName { get; set; } = "Upgrade base attack";
    public string Description { get; set; } = "20% increase attack speed and +5 dmg!";
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
        float newCooldown;
        switch (CurrentLevel)
        {
            case 1:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 2:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 3:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 4:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 5:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 6:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 7:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
            case 8:
                newCooldown = character.BaseAttackCooldown - character.BaseAttackCooldown * 0.2f;
                character.UpdateAttackCooldown(newCooldown);
                character.BaseAttackDamage += 5f;
                GD.Print("Cooldown: " + character.BaseAttackCooldown);
                break;
        }
    }

    private void SetupNextLevelAbilityResource()
    {
        switch (CurrentLevel)
        {
            case 2:
                Description = "20% increase attack speed and +5 dmg!";
                break;
            case 3:
                Description = "20% increase attack speed and +5 dmg!";
                break;
            case 4:
                Description = "20% increase attack speed and +5 dmg!";
                break;
            case 5:
                Description = "20% increase attack speed and +5 dmg!";
                break;
            case 6:
                Description = "20% increase attack speed and +5 dmg!";
                break;
            case 7:
                Description = "20% increase attack speed and +5 dmg!";
                break;
            case 8:
                Description = "20% increase attack speed and +5 dmg!";
                break;
        }
    }
}
