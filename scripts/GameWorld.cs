using Godot;
using System.Collections.Generic;

public partial class GameWorld : Node2D
{
	[Export]
	public Hud Hud { get; set; }
	[Export]
	public AbilityUi AbilityUi { get; set; }
	[Export]
	public Node2D Level { get; set; }
	[Export]
	public ExperienceManager ExperienceManager { get; set; }
	[Export]
	public AbilityManager AbilityManager { get; set; }

	public BaseCharacter Character { get; set; }

	private SceneManager SceneManager => GetNode<SceneManager>("/root/SceneManager"); 
	private List<IAbility> _currentIAbilities = new();
	private Node2D _currentLevel;

	public override void _Ready()
	{
		PackedScene currentScene = ResourceLoader.Load<PackedScene>(SceneManager.CurrentLevel);
		_currentLevel = (Node2D)currentScene.Instantiate();
		Character = this.GetCharacter();
		AddChild(_currentLevel);
		AddChild(Character);

		Hud.SetCurrentExperienceLevel(ExperienceManager.CurrentExperienceLevel);

		Character.ExperiencePickedup += HandleExperienceChange;
		Character.OnCharacterDied += HandleCharacterDeath;
		ExperienceManager.LevelIncrease += HandleExperienceLevelIncrease;
		AbilityUi.AbilitySelected += HandleAbilitySelection;
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
		if (!AbilityManager.CurrentAbilities.Contains(selectedAbility))
		{
			AbilityManager.CurrentAbilities.Add(selectedAbility);
			selectedAbility.Apply(Character);
		}
		selectedAbility.Upgrade();
		AbilityUi.ClearAbilities();
	}

	private void HandleCharacterDeath()
	{
		SceneManager.ChangeSceneToWaitingRoom();
		SceneManager.CurrentLevel = null;
	}
}
