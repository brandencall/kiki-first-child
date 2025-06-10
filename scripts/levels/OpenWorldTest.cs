using Godot;
using System;

public partial class OpenWorldTest : Node2D
{
	[Export]
	public SpawnManager SpawnManager { get; set; }
	[Export]
	public string WaveConfig { get; set; }

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager");

	public override void _Ready()
	{
		SpawnManager.Initialize(WaveConfig);
		SpawnManager.WaveFinished += OnWaveFinished;
	}

	private void OnWaveFinished(WaveInfo wave)
	{
		GameManager.DepositCurrenct(wave.CurrencyEarned);
	}

}
