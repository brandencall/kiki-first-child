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
        MainMenu.Visible = false;
        GameWorld gameWorld = GameScene.Instantiate<GameWorld>();
        gameWorld.GameFinished += OnGameFinished;
        AddChild(gameWorld);
    }

    private void OnGameFinished()
    {
        MainMenu.Visible = true; 
    }

}
