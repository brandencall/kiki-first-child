using Godot;
using System;

public partial class SceneManager : Node
{
	public string CurrentScene { get; private set; }
	public LevelData CurrentLevel { get; set; }

	public void ChangeSceneToWaitingRoom()
	{
		string waitingRoomScene = "res://scenes/levels/waiting_room/waiting_room.tscn";
		GetTree().CallDeferred("change_scene_to_file", waitingRoomScene); 
		CurrentScene = waitingRoomScene;
	}

	public void ChangeScene(string scene)
	{
		try
		{
			GetTree().CallDeferred("change_scene_to_file", scene);
			CurrentScene = scene;
		}
		catch
		{
			GD.PrintErr("Need to pass a valid scene");
		}
	}
}
