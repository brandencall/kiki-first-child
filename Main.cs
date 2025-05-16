using Godot;

public partial class Main : Node
{
	[Export]
	public MainMenu MainMenu { get; set; }

	private SceneManager SceneManager => GetNode<SceneManager>("/root/SceneManager"); 

	public override void _Ready()
	{
		MainMenu.StartGame += StartGame;
	}

	public void StartGame()
	{
		SceneManager.ChangeSceneToWaitingRoom();
	}

	private void OnGameFinished()
	{
		MainMenu.Visible = true; 
	}

}
