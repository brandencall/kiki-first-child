using Godot;
using System.Collections.Generic;

public partial class GameWorld : Node2D
{
	[Export]
	public Hud Hud { get; set; }
	[Export]
	public AbilityUi AbilityUi { get; set; }
	[Export]
	public ExperienceManager ExperienceManager { get; set; }
	[Export]
	public AbilityManager AbilityManager { get; set; }

	public BaseCharacter Character { get; set; }

	private SceneManager SceneManager => GetNode<SceneManager>("/root/SceneManager"); 
	private GameManager GameManager => GetNode<GameManager>("/root/GameManager"); 
	private List<IAbility> _currentIAbilities = new();
	//Need to change this to a BaseLevel class to handle multiple levels
	private OpenWorldTest _level;

	public override void _Ready()
	{
		PackedScene currentScene = ResourceLoader.Load<PackedScene>(SceneManager.CurrentLevel.Scene);
		_level = (OpenWorldTest)currentScene.Instantiate();
		Character = GameManager.CurrentCharacter;
		AddChild(_level);
		AddChild(Character);

		Hud.SetCurrentExperienceLevel(ExperienceManager.CurrentExperienceLevel);

		Character.ExperiencePickedup += HandleExperienceChange;
		ExperienceManager.LevelIncrease += HandleExperienceLevelIncrease;
		AbilityUi.AbilitySelected += HandleAbilitySelection;
		_level.OnLevelFinished += HandleLevelFinished;
	}

	private void HandleExperienceChange(float experience)
	{
		ExperienceManager.IncreaseExperience(experience);
		Hud.SetExperience(ExperienceManager.CurrentExperience);
	}

	private void HandleExperienceLevelIncrease()
	{
		List<IAbility> iAbility = AbilityManager.GetRandomAbilities();

		foreach (var ability in iAbility)
		{
			AbilityUi.AddAbilityCard(ability);
		}

		AbilityUi.ShowAbilities();
		Hud.SetMaxExperience(ExperienceManager.MaxExperienceForLevel);
		Hud.SetCurrentExperienceLevel(ExperienceManager.CurrentExperienceLevel);
	}

	private void HandleAbilitySelection(IAbility selectedAbility)
	{
		selectedAbility.Upgrade(Character);
		AbilityUi.ClearAbilities();
	}

	private void HandleLevelFinished()
	{
		CallDeferred("remove_child", Character);
		GameManager.CharacterDied(Character);
		SceneManager.ChangeSceneToWaitingRoom();
		SceneManager.CurrentLevel = null;
	}

}
