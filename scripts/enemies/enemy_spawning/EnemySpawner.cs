using Godot;
using System;

public partial class EnemySpawner : Node
{
    private Random _random = new Random();
    private CharacterBody2D _player;
    private PathFollow2D _spawnPath;
    private Marker2D _spawnMarker;

    public override void _Ready()
    {
        CallDeferred(nameof(InitPlayer));
    }

    private void InitPlayer()
    {
        _player = this.GetPlayer();
        if (_player != null)
        {
            Path2D path = _player.GetNode<Path2D>("EnemySpawnPath");
            _spawnPath = path.GetChild<PathFollow2D>(0);
            _spawnMarker = _spawnPath.GetChild<Marker2D>(0);
        }
    }   

    public void SpawnEnemy(PackedScene enemyScene)
    {
        if (enemyScene == null) return;

        CharacterBody2D enemy = enemyScene.Instantiate<CharacterBody2D>();
        enemy.Position = SpawnLocation();
        GetParent().AddChild(enemy);
    }

    private Vector2 SpawnLocation()
    {
        return SpawnEnemyOutsideOfCamera();
    }

    private Vector2 SpawnEnemyOutsideOfCamera()
    {
        if (_spawnPath == null) return new Vector2(0, 0);

        _spawnPath.ProgressRatio = (float)_random.NextDouble();
        return _spawnMarker.GlobalPosition;
    }
}
