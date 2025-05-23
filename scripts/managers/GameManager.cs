using Godot;
using System.Collections.Generic;
using System.Diagnostics;

public partial class GameManager : Node
{
	public BaseCharacter CurrentCharacter { get; set; }
	public List<CharacterData> CharacterDataList { get; set; }
	public string CurrentScene { get; set; }
	public GameState GameStateData { get; set; }

	public void Initialize()
	{
		Debug.Assert(GameStateData != null, "The GamestateData must be initialized in GameManager");
		Debug.Assert(CharacterDataList != null, "The CharacterDataList must be initialized in GameManager");

		if (GameStateData.LastUsedCharacter != null)
		{
			CharacterData currentCharacterData = GameStateData.LastUsedCharacter;
			PackedScene characterScene = GD.Load<PackedScene>(currentCharacterData.Scene);
			CurrentCharacter = characterScene.Instantiate<BaseCharacter>();
			CurrentCharacter.Initialize(currentCharacterData);
		}
		else
		{
			CharacterData currentCharacterData = CharacterDataList[0];
			PackedScene characterScene = GD.Load<PackedScene>(currentCharacterData.Scene);
			CurrentCharacter = characterScene.Instantiate<BaseCharacter>();
			CurrentCharacter.Initialize(currentCharacterData);
		}
	}
}
