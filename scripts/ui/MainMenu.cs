using Godot;

public partial class MainMenu : Control
{
    [Export]
    public Button StartButton { get; set; }

	private SceneManager SceneManager => GetNode<SceneManager>("/root/SceneManager"); 

    public void OnStartButtonPressed()
    {
        SceneManager.ChangeSceneToWaitingRoom();
    }

    public void OnOptionPressed()
    {
    }

    public void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
