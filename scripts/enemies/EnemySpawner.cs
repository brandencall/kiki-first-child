using Godot;
using System;

public partial class EnemySpawner : Node
{
    [Export]
    public PackedScene EnemyScene;
    private Random _random = new Random();
    private CharacterBody2D _player;
    private PathFollow2D _spawnPath;
    private Marker2D _spawnMarker;

    public override void _Ready()
    {
        var timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
        _player = this.GetPlayer();
        if (_player != null)
        {
            Path2D path = _player.GetNode<Path2D>("EnemySpawnPath");
            _spawnPath = path.GetChild<PathFollow2D>(0);
            _spawnMarker = _spawnPath.GetChild<Marker2D>(0);
        }
    }

    private void OnTimerTimeout()
    {
        SpawnEnemy();
    }

    //TODO: Create a spawn manager that hands this what kind of enemy to spawn.
    //Make a method for spawning enemies off camera and one for on camera?
    public void SpawnEnemy()
    {
        if (EnemyScene == null) return;

        var enemy = EnemyScene.Instantiate<TorchEnemy>();
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
