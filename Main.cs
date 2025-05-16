using Godot;

public partial class Main : Node
{
	[Export]
	public PackedScene GameScene { get; set; }
	[Export]
	public MainMenu MainMenu { get; set; }
	[Export]
	public PackedScene WaitingRoomScene { get; set; }

	private SaveManager SaveManager => GetNode<SaveManager>("/root/SaveManager");
	private WaitingRoom _waitingRoom;

	public override void _Ready()
	{
		MainMenu.StartGame += StartGame;
	}

	public void StartGame()
	{
		MainMenu.Visible = false;
		_waitingRoom = WaitingRoomScene.Instantiate<WaitingRoom>();
		_waitingRoom.LevelSelected += OnLevelSelected;
		AddChild(_waitingRoom);
	}

	private void OnLevelSelected()
	{
		GameWorld gameWorld = GameScene.Instantiate<GameWorld>();
		gameWorld.GameFinished += OnGameFinished;
		gameWorld.Character = _waitingRoom.currentCharacter;
		GetTree().CallDeferred("change_scene_to_file", "res://scenes/game_world.tscn");
		GD.Print("on level selected: " + gameWorld.Character);
	}

	private void OnGameFinished()
	{
		MainMenu.Visible = true; 
	}

}
