using Godot;

public partial class TooFastTooFurious : Node, IAbility
{
	public string AbilityName { get; set; } = "TooFastTooFurious";
	public string Description { get; set; } = "The faster they move the more damage you do";
	public Texture2D AbilityIcon { get; set; } = ResourceLoader.Load<Texture2D>("res://assets/icon.svg");
	public int CurrentLevel { get; set; } = 1;
	public bool OnLastLevel { get; set; } = false;

	private PackedScene _tooFastTooFuriousScene = ResourceLoader.Load<PackedScene>("res://scenes/abilities/too_fast_too_furious/too_fast_too_furious.tscn");
	private TooFastTooFuriousAbility _tooFastTooFurious;

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
				_tooFastTooFurious = _tooFastTooFuriousScene.Instantiate<TooFastTooFuriousAbility>();
				character.AddChild(_tooFastTooFurious);
				character.Effects.Add(_tooFastTooFurious);
				break;
			case 2:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
			case 3:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
			case 4:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
			case 5:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
			case 6:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
			case 7:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
			case 8:
				_tooFastTooFurious.DamageMultiplier += 0.05f;
				break;
		}
	}

	private void SetupNextLevelAbilityResource()
	{
		switch (CurrentLevel)
		{
			case 2:
				Description = "+5% more dmg based on speed";
				break;
			case 3:
				Description = "+5% more dmg based on speed";
				break;
			case 4:
				Description = "+5% more dmg based on speed";
				break;
			case 5:
				Description = "+5% more dmg based on speed";
				break;
			case 6:
				Description = "+5% more dmg based on speed";
				break;
			case 7:
				Description = "+5% more dmg based on speed";
				break;
			case 8:
				Description = "+5% more dmg based on speed";
				break;
		}
	}
}
