using Godot;
using System.Collections.Generic;

public partial class GameWorld : Node2D
{
	[Export]
	public Hud Hud { get; set; }
	[Export]
	public AbilityUi AbilityUi { get; set; }
	[Export]
	public LevelManager LevelManager { get; set; }
	[Export]
	public Node2D Levels { get; set; }
	[Export]
	public BaseCharacter Character{ get; set; }
	[Export]
	public ExperienceManager ExperienceManager { get; set; }
	[Export]
	public AbilityManager AbilityManager { get; set; }

	[Signal]
	public delegate void GameFinishedEventHandler();

	private List<IAbility> _currentIAbilities = new();

	private Node2D _currentLevel;

	public override void _Ready()
	{
		PackedScene currentScene = ResourceLoader.Load<PackedScene>(LevelManager.CurrentLevel);
		_currentLevel = (Node2D)currentScene.Instantiate();
		Levels.AddChild(_currentLevel);

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
		EmitSignal(SignalName.GameFinished);
		QueueFree();
	}
}
