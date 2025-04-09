using Godot;
using System;

public partial class EnemySpawner : Node
{
    [Export]
    public PackedScene EnemyScene;
    private Random _random = new Random();
    private CharacterBody2D _player;
    private Vector2 _cameraBounds;

    public override void _Ready()
    {
        var timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
        _player = this.GetPlayer();
        _cameraBounds = GetCameraBounds();
    }

    private Vector2 GetCameraBounds()
    {
        Camera2D camera = _player.GetNode<Camera2D>("Camera2D"); 
        if (camera != null)
        {
            GD.Print("Player has a camera");

            var screenSize = camera.GetViewportRect().Size;
            Vector2 zoomedSize = screenSize / camera.Zoom;
            Vector2 topLeft = camera.GlobalPosition - zoomedSize / 2;
            return topLeft;
        }
        else
        {
            GD.Print("No camera");
            return new Vector2(0, 0);
        }
    }

    private void OnTimerTimeout()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (EnemyScene == null) return;

        var enemy = EnemyScene.Instantiate<TorchEnemy>();
        enemy.Position = SpawnLocation();
        GetParent().AddChild(enemy);
    }

    private Vector2 SpawnLocation()
    {
        return RandomSpawnLocation(_cameraBounds);
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

        return new Vector2(randomX, randomY);
    }
}
