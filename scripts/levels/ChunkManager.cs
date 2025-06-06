using Godot;
using System.Collections.Generic;

public partial class ChunkManager : Node2D
{
    public BaseCharacter Character { get; set; }
    [Export]
    public WorldTileSet TileMap { get; set; }
    [Export]
    public LowResFlowFieldDebug LowResDebugger { get; set; }
    [Export]
    public HighResFlowFieldDebug HighResDebugger { get; set; }
    [Export]
    public FlowFieldManager FlowField { get; set; }
    [Export]
    public int chunkSize = 8;
    [Export]
    public int renderDistance = 2;
    public int tileSize = 64;

    private Vector2I _currentChunk;
    private Vector2I _previousChunk;
    private HashSet<Vector2I> _activeChunks = new();
    private Vector2I _currentGridCell;
    private Vector2I _previousGridCell;

    public override void _Ready()
    {
        Character = this.GetCharacter();
        _currentChunk = (Vector2I)CallDeferred(nameof(GetCurrentChunk), Character.GlobalPosition);
        _currentGridCell = TileMap.LocalToMap(Character.GlobalPosition);
        _activeChunks.Add(_currentChunk);
        FlowField.GenerateHighResField(_currentChunk, Character.GlobalPosition);
        //LowResDebugger.SetFlowVectors(FlowField.ChunkDirectionMap);
        TileMap.Generate(_currentChunk, chunkSize);
        RenderChunk();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        _currentGridCell = TileMap.LocalToMap(Character.GlobalPosition);
        _currentChunk = GetCurrentChunk(Character.GlobalPosition);

        if (_currentGridCell != _previousGridCell)
        {
            FlowField.GenerateHighResField(_currentChunk, Character.GlobalPosition);
            //HighResDebugger.SetFlowVectors(FlowField.DetailedFlowFields);
            _previousGridCell = _currentGridCell;
        }
        UpdateLowResFlowField();
    }

    private void UpdateLowResFlowField()
    {
        FlowField.ValidateChunkMapWithCurrent(_currentChunk);
        if (_currentChunk != _previousChunk)
        {
            RenderChunk();
            _previousChunk = _currentChunk;
        }
        //        else
        //        {
        //            FlowField.UpdateLowResField(Character.GlobalPosition);
        //            //LowResDebugger.SetFlowVectors(FlowField.ChunkDirectionMap);
        //        }
        //LowResDebugger.SetFlowVectors(FlowField.ChunkDirectionMap);
    }

    public Vector2I GetCurrentChunk(Vector2 position)
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
                //FlowField.GenerateLowResField(chunk, _currentChunk);
                FlowField.GenerateLowResField(chunk, Character.GlobalPosition, _currentChunk);
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
            FlowField.ChunkDirectionMap.Remove(chunk);
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
