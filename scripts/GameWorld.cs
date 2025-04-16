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

    private Node2D _currentLevel;

    public override void _Ready()
    {
        PackedScene currentScene = ResourceLoader.Load<PackedScene>(LevelManager.CurrentLevel);
        _currentLevel = (Node2D)currentScene.Instantiate();
        Levels.AddChild(_currentLevel);

        Player.ExperiencePickedup += HandleExperienceChange;
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
    }

    private void HandleAbilitySelection(AbilityResource selectedAbility)
    {
        AbilityUi.ClearAbilities();
    }
}
