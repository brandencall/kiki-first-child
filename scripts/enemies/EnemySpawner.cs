using Godot;
using System;

public partial class EnemySpawner : Node
{
    [Export]
    public PackedScene EnemyScene;
    private Random _random = new Random();
    private Player _player;

    public override void _Ready()
    {
        var timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
        _player = (Player)GetTree().GetFirstNodeInGroup("players");
    }

    private void OnTimerTimeout()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (EnemyScene == null) return;

        var enemy = EnemyScene.Instantiate<Enemy>();
        enemy.Initialize(_player);
        enemy.Position = SpawnLocation();
        GetParent().AddChild(enemy);
    }

    private Vector2 SpawnLocation()
    {
        Vector2 bounds = _player.GetCameraBounds();
        return RandomSpawnLocation(bounds);
    }

    private Vector2 RandomSpawnLocation(Vector2 cameraBounds)
    {
        int distanceOutsideScreen = 100;
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        float randomX, randomY;

        if (rng.Randi() % 2 == 0)
        {
            // Top or Bottom
            randomX = rng.RandiRange(-(int)cameraBounds.X, (int)cameraBounds.X);
            randomY = rng.Randi() % 2 == 0
                ? cameraBounds.Y - distanceOutsideScreen
                : -cameraBounds.Y + distanceOutsideScreen;
        }
        else
        {
            // Left or Right
            randomY = rng.RandiRange(-(int)cameraBounds.Y, (int)cameraBounds.Y);
            randomX = rng.Randi() % 2 == 0
                ? cameraBounds.X - distanceOutsideScreen        
                : -cameraBounds.X + distanceOutsideScreen;        
        }

        GD.Print("randomX: " + randomX + ", randomY: " + randomY);

        return new Vector2(randomX, randomY);
    }
}
