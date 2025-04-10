using Godot;
using System;

public partial class EnemySpawner : Node
{
    [Export]
    public PackedScene EnemyScene;
    private Random _random = new Random();
    private CharacterBody2D _player;
    private Camera2D _camera;

    //TODO: This is NOT the right way to do this. But does make the enemies spawn outside of camera.
    //Might need to do something similar to this: https://www.youtube.com/watch?v=7rijR1_PBBQ
    private Vector2 _topLeftCamera = new Vector2(0, 0);
    private Vector2 _topRightCamera = new Vector2(0, 0);
    private Vector2 _bottomLeftCamera = new Vector2(0, 0);
    private Vector2 _bottomRightCamera = new Vector2(0, 0);

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == NotificationWMSizeChanged)
        {
            var screenSize = GetViewport().GetVisibleRect().Size;
            GD.Print("screenSize: " + screenSize);
        }
    }

    public override void _Ready()
    {
        var timer = GetNode<Timer>("Timer");
        timer.Timeout += OnTimerTimeout;
        _player = this.GetPlayer();
        if (_player != null)
        {
            _camera = _player.GetNode<Camera2D>("Camera2D");
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
        GetCameraBounds();
        return RandomSpawnLocation();
    }

    private void GetCameraBounds()
    {
        if (_camera != null)
        {
            var screenSize = GetViewport().GetVisibleRect().Size;
            Vector2 zoomedSize = screenSize / _camera.Zoom;
            _topLeftCamera = _camera.GlobalPosition - zoomedSize / 2;
            _topRightCamera = new Vector2(_topLeftCamera.X + zoomedSize.X, _topLeftCamera.Y);
            _bottomLeftCamera = new Vector2(_topLeftCamera.X, _topLeftCamera.Y + zoomedSize.Y);
            _bottomRightCamera = new Vector2(_topLeftCamera.X + zoomedSize.X, _topRightCamera.Y + zoomedSize.Y);
        }
    }

    private Vector2 RandomSpawnLocation()
    {
        int distanceOutsideScreen = 100;
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        float randomX, randomY;

        if (rng.Randi() % 2 == 0)
        {
            // Top or Bottom
            randomX = rng.RandiRange((int)_topLeftCamera.X, (int)_topRightCamera.X);
            randomY = rng.Randi() % 2 == 0 ? _topLeftCamera.Y - distanceOutsideScreen : _bottomLeftCamera.Y + distanceOutsideScreen;
        }
        else
        {
            // Left or Right
            randomY = rng.RandiRange((int)_topLeftCamera.Y, (int)_bottomLeftCamera.Y);
            randomX = rng.Randi() % 2 == 0 ? _topLeftCamera.X - distanceOutsideScreen : _topRightCamera.X + distanceOutsideScreen;
        }

        return new Vector2(randomX, randomY);
    }
}
