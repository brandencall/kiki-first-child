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
    public BasePlayer Player { get; set; }
    [Export]
    public ExperienceManager ExperienceManager { get; set; }
    [Export]
    public AbilityManager AbilityManager { get; set; }

    [Signal]
    public delegate void GameFinishedEventHandler();

    private List<AbilityResource> _currentAbilities = new();

    private Node2D _currentLevel;

    public override void _Ready()
    {
        PackedScene currentScene = ResourceLoader.Load<PackedScene>(LevelManager.CurrentLevel);
        _currentLevel = (Node2D)currentScene.Instantiate();
        Levels.AddChild(_currentLevel);

        Hud.SetCurrentExperienceLevel(ExperienceManager.CurrentExperienceLevel);

        Player.ExperiencePickedup += HandleExperienceChange;
        Player.OnPlayerDied += HandlePlayerDeath;
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
        List<AbilityResource> abilities = AbilityManager.GetRandomAbilities();

        foreach (var ability in abilities)
        {
            AbilityUi.AddAbilityCard(ability);
        }
        AbilityUi.ShowAbilities();
        Hud.SetMaxExperience(ExperienceManager.MaxExperienceForLevel);
        Hud.SetCurrentExperienceLevel(ExperienceManager.CurrentExperienceLevel);
    }

    private void HandleAbilitySelection(AbilityResource selectedAbility)
    {
        if (selectedAbility.AbilityLogic == null)
        {
            selectedAbility.InitializeAbilityLogic();
            _currentAbilities.Add(selectedAbility);
        }
        AbilityLogic ability = selectedAbility.AbilityLogic;
        ability.Upgrade();
        ability.Apply(Player);
        AbilityUi.ClearAbilities();
    }

    private void HandlePlayerDeath()
    {
        EmitSignal(SignalName.GameFinished);
        QueueFree();
    }
}
