using Godot;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class SpawnManager : Node
{
	[Export]
	private string _enemyConfigString;
	[Export]
	private EnemySpawner _enemySpawner;

	private Random _random = new Random();
	private float _timeBetweenWaves = 2.0f;

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager"); 

	public override void _Ready()
	{
		var jsonFile = FileAccess.Open(_enemyConfigString, FileAccess.ModeFlags.Read);
		string jsonText = jsonFile.GetAsText();
		List<WaveInfo> waves = JsonConvert.DeserializeObject<List<WaveInfo>>(jsonText);

		Start(waves);
	}

	private async void Start(List<WaveInfo> waves)
	{
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		foreach (var wave in waves)
		{
			await StartWave(wave);
			await ToSignal(GetTree().CreateTimer(_timeBetweenWaves, true), "timeout");
			GameManager.GameStateData.Schmeckels += wave.CurrencyEarned;
			GD.Print(GameManager.GameStateData.Schmeckels);
		}
	}

	private async Task StartWave(WaveInfo wave)
	{
		bool validTotalProbability = wave.ValidateSpawnProbability();
		if (!validTotalProbability) GD.PrintErr("Probability not valid");

		for (int i = 0; i < wave.TotalEnemies; i++)
		{
			await WaitUntilUnpaused();
			EnemyConfig enemy = GetRandomEnemy(wave.Enemies);
			PackedScene enemyScene = GD.Load<PackedScene>(enemy.Scene);
			_enemySpawner.SpawnEnemy(enemyScene);
			await ToSignal(GetTree().CreateTimer(wave.SpawnInterval, true), "timeout");
		}
	}

	private async Task WaitUntilUnpaused()
	{
		while (GetTree().Paused)
		{
			await ToSignal(GetTree(), "process_frame");
		}
	}

	private EnemyConfig GetRandomEnemy(List<EnemyConfig> enemies)
	{
		float roll = (float)_random.NextDouble();
		float cumulative = 0.0f;

		foreach (var enemy in enemies)
		{
			cumulative += enemy.Probability;
			if (roll <= cumulative)
			{
				return enemy;
			}
		}

		return enemies[0];
	}
}
