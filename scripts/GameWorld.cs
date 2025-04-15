using Godot;

public partial class GameWorld : Node2D
{
    [Export]
    public Hud Hud { get; set; }
    [Export]
    public LevelManager LevelManager { get; set; }
    [Export]
    public Node2D Levels { get; set; }
    // Need to change this to a generic Player object.
    [Export]
    public PurpleKnightPlayer Player { get; set; }
    private Node2D _currentLevel;

    public override void _Ready()
    {
        PackedScene currentScene = ResourceLoader.Load<PackedScene>(LevelManager.CurrentLevel);
        _currentLevel = (Node2D)currentScene.Instantiate();
        Levels.AddChild(_currentLevel);

        Player.ExperiencePickedup += HandleExperienceChange;
    }

    private void HandleExperienceChange(float experience)
    {
        GD.Print("experience signal recieved");
        Hud.IncreaseExperience(experience);
    }
}
