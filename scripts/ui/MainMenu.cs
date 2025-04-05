using Godot;

public partial class MainMenu : Control
{
    [Export]
    public Button StartButton { get; set; }

    [Signal]
    public delegate void StartGameEventHandler();

    public void OnStartButtonPressed()
    {
        EmitSignal(SignalName.StartGame);
        QueueFree();
    }

    public void OnOptionPressed()
    {
    }

    public void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
