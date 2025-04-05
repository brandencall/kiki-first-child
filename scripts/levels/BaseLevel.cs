using Godot;

public abstract partial class BaseLevel : Node2D
{
    public abstract int NumberOfWaves { get; }
    public abstract int NumberOfEnemiesPerWave { get; }

}
