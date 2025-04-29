using Godot;
using System.Collections.Generic;

public partial class ChunkManager : Node2D
{
    public BasePlayer Player { get; set; }
    [Export]
    public WorldTileSet TileMap { get; set; }
    [Export]
    public FlowFieldDebug Debugger { get; set; }
    [Export]
    public FlowFieldManager FlowField { get; set; }
    [Export]
    public Timer ChunkLoadTimer { get; set; }
    [Export]
    public int chunkSize = 8;
    [Export]
    public int renderDistance = 2;
    public int tileSize = 64;

    private Vector2I _currentChunk;
    private Vector2I _previousChunk;
    private HashSet<Vector2I> _activeChunks = new();

    public override void _Ready()
    {
        Player = this.GetPlayer();
        _currentChunk = (Vector2I)CallDeferred(nameof(GetCurrentChunk), Player.GlobalPosition);
        _activeChunks.Add(_currentChunk);
        TileMap.Generate(_currentChunk, chunkSize);
        ChunkLoadTimer.Timeout += OnChunkLoadTimerTimeOut;
        RenderChunk();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        FlowField.GenerateHighResField(_currentChunk, Player.GlobalPosition);
        Debugger.SetDetailedFlow(FlowField.DetailedFlowFields);
    }

    private void OnChunkLoadTimerTimeOut()
    {
        _currentChunk = GetCurrentChunk(Player.GlobalPosition);
        FlowField.GenerateHighResField(_currentChunk, Player.GlobalPosition);
        if (_currentChunk != _previousChunk)
        {
            RenderChunk();
            //Debugger.SetFlowVectors(FlowField._chunkDirectionMap);
        }

        _previousChunk = _currentChunk;
    }

    private Vector2I GetCurrentChunk(Vector2 position)
    {
        Vector2I result;
        result.X = (int)(position.X / (chunkSize * tileSize));
        result.Y = (int)(position.Y / (chunkSize * tileSize));
        if (position.X < 0)
        {
            result.X -= 1;
        }
        if (position.Y < 0)
        {
            result.Y -= 1;
        }
        return result;
    }

    private void RenderChunk()
    {
        float totalChunksToRender = ((renderDistance * 2f) + 1f);
        List<Vector2I> chunksToDelete = new();

        for (int x = 0; x < totalChunksToRender; x++)
        {
            int chunkX = renderDistance + _currentChunk.X - x;
            int chunkPositionX = chunkX * chunkSize;
            for (int y = 0; y < totalChunksToRender; y++)
            {
                int chunkY = renderDistance + _currentChunk.Y - y;
                int chunkPositionY = chunkY * chunkSize;

                Vector2I chunk = new Vector2I(chunkX, chunkY);
                FlowField.GenerateLowResField(chunk, _currentChunk);
                if (!_activeChunks.Contains(chunk))
                {
                    TileMap.Generate(new Vector2I(chunkPositionX, chunkPositionY), chunkSize);
                    _activeChunks.Add(chunk);
                }
            }
        }

        foreach (var chunk in _activeChunks)
        {
            if (Mathf.Abs(chunk.X - _currentChunk.X) > renderDistance
                        || Mathf.Abs(chunk.Y - _currentChunk.Y) > renderDistance)
            {
                Vector2I chunkToRemove = new Vector2I(chunk.X * chunkSize, chunk.Y * chunkSize);
                TileMap.ClearChunk(chunkToRemove, chunkSize);
                chunksToDelete.Add(chunk);
            }
        }

        foreach (var chunk in chunksToDelete)
        {
            _activeChunks.Remove(chunk);
        }
    }

    public List<Vector2I> GetTilesInChunk(Vector2I chunkCoor)
    {
        return TileMap.GetTilesInChunk(chunkCoor * chunkSize, chunkSize);
    }

    public Vector2 GetChunkCenter(Vector2I chunkCoor)
    {
        var chunkX = chunkCoor.X * chunkSize * tileSize;
        var chunkY = chunkCoor.Y * chunkSize * tileSize;
        Vector2I chunkCenterTile = new Vector2I(chunkX, chunkY) + new Vector2I((chunkSize / 2) * tileSize, (chunkSize / 2) * tileSize);
        return TileMap.LocalToMap(chunkCenterTile);
    }
}
