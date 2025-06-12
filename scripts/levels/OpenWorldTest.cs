using Godot;
using System;
using System.Collections.Generic;

public partial class OpenWorldTest : Node2D
{
	[Export]
	public SpawnManager SpawnManager { get; set; }
	[Export]
	public string WaveConfig { get; set; }

	[Signal]
	public delegate void OnLevelFinishedEventHandler();

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager");
	private RoundStats _roundStats;
	private BaseCharacter _character;

	public override void _Ready()
	{
		SpawnManager.Initialize(WaveConfig);
		SpawnManager.WaveFinished += OnWaveFinished;
		SpawnManager.EnemySpawned += OnEnemySpawn;
		_roundStats = new();
		_roundStats.LevelName = "OpenWorldTest";
		_character = GameManager.CurrentCharacter;
		_character.OnCharacterDied += HandleCharacterDeath;
	}

	private void OnWaveFinished(WaveInfo wave)
	{
		GameManager.DepositCurrenct(wave.CurrencyEarned);
		_roundStats.RoundsSurvived = wave.Wave;
	}

	private void OnEnemySpawn(BaseEnemy enemy)
	{
		enemy.OnEnemyDied += OnEnemyDied;
	}

	private void OnEnemyDied()
	{
		_roundStats.EnemiesKilled += 1;
	}

	private void HandleCharacterDeath()
	{
		GD.Print("the character died. This is where we show the player death ui");
		GD.Print("Stats: ");
		GD.Print("Enemies Killed: ", _roundStats.EnemiesKilled);
		GD.Print("Rounds Survived: ", _roundStats.RoundsSurvived);
		HandleLevelFinished();
	}

	private void HandleLevelFinished()
	{
		EmitSignal(SignalName.OnLevelFinished);
	}
}
