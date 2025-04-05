using Godot;

public partial class GameWorld : Node2D
{
    [Export]
    public LevelManager LevelManager { get; set; }
    [Export]
    public Node2D Levels { get; set; }
    private Node2D _currentLevel;

    public override void _Ready()
    {
        PackedScene currentScene = ResourceLoader.Load<PackedScene>(LevelManager.CurrentLevel);
        _currentLevel = (Node2D)currentScene.Instantiate();
        Levels.AddChild(_currentLevel);
    }
}
