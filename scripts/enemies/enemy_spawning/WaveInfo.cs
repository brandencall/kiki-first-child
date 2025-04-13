using Godot;
using System.Collections.Generic;
using Newtonsoft.Json;

[GlobalClass]
public partial class WaveInfo : Resource
{
    public int Wave { get; set; }
    [JsonProperty("spawn_interval")]
    public float SpawnInterval { get; set; }
    [JsonProperty("total_enemies")]
    public int TotalEnemies { get; set; }
    public List<EnemyConfig> Enemies { get; set; }

    public bool ValidateSpawnProbability()
    {
        float result = 0.0f;
        foreach (var enemy in Enemies)
        {
            result += enemy.Probability;
        }
        return result == 1.0f;
    }
}
