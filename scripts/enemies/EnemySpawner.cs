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
        Vector2 playerPostion = _player.Position;
        return new Vector2(
                playerPostion.X + GetRandomNumber(),
                playerPostion.Y + GetRandomNumber()
                );
    }

    private int GetRandomNumber()
    {
        int randomNumber;
        int minNumber = 650;
        int maxNumber = 650;
        if (_random.Next(0, 2) == 0)
        {
            randomNumber = _random.Next(minNumber, maxNumber);
        }
        else
        {
            randomNumber = _random.Next(-maxNumber, minNumber);
        }
        return randomNumber;
    }
}
