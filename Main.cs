using Godot;

public partial class Main : Node
{
    [Export]
    public PackedScene GameScene { get; set; }
    [Export]
    public MainMenu MainMenu { get; set; }

    public override void _Ready()
    {
        MainMenu.StartGame += StartGame;
    }

    public void StartGame()
    {
        Node2D gameWorld = (Node2D)GameScene.Instantiate();
        AddChild(gameWorld);
    }

}
