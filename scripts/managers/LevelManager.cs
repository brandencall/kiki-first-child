using Godot;
using System.Collections.Generic;

public partial class LevelManager : Node
{
    [Export]
    private Player _player { get; set; }
    public string CurrentLevel { get; private set; }
    private List<string> _levelPaths = new()
    {
        "res://scenes/levels/test_level.tscn",
    };
    private int _currentLevelIndex;

    public override void _Ready()
    {
        _currentLevelIndex = 0;
        CurrentLevel = _levelPaths[_currentLevelIndex];
        _player.PlayerDied += OnPlayerDied;
    }

    public string GetNextLevel()
    {
        _currentLevelIndex++;
        if (_currentLevelIndex > _levelPaths.Count)
        {
            GD.Print("Need to handle end of game.");
            return "";
        }
        else 
        {
            return _levelPaths[_currentLevelIndex];
        }
    }

    private void OnPlayerDied()
    {
        GD.Print("Play death screen");
    }
}
