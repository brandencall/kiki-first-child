using Godot;
using System.Collections.Generic;

public partial class FlowFieldManager : Node2D
{
    [Export]
    public ChunkManager ChunkManager { get; set; }
    public Dictionary<Vector2I, Vector2> _chunkDirectionMap = new();
    public Dictionary<Vector2I, Dictionary<Vector2I, Vector2>> _detailedFlowFields = new();
    public Dictionary<Vector2I, Vector2> DetailedFlowFields = new();
    private Dictionary<Vector2I, Vector2> _flowChart = new();
    private Dictionary<Vector2I, int> _costField = new();
    public int tileSize = 64;

    public void GenerateHighResField(Vector2I chunkCoord, Vector2 playerPos)
    {
        DetailedFlowFields.Clear();
        var flowField = new Dictionary<Vector2I, Vector2>();
        Vector2I goalGrid = ChunkManager.TileMap.LocalToMap(playerPos);
         
        List<Vector2I> localTiles = ChunkManager.GetTilesInChunk(chunkCoord);

        if (!localTiles.Contains(goalGrid)) return;

        Dictionary<Vector2I, int> costField = new();
        Queue<Vector2I> openQueue = new();

        openQueue.Enqueue(goalGrid);
        costField[goalGrid] = 0;

        while (openQueue.Count > 0)
        {
            Vector2I current = openQueue.Dequeue();
            int currentCost = costField[current];

            foreach (Vector2I neighbor in GetWalkableNeighbors(current))
            {
                if (!localTiles.Contains(neighbor))
                {
                    continue;
                }
                if (!costField.ContainsKey(neighbor) || costField[neighbor] > currentCost + 1)
                {
                    costField[neighbor] = currentCost + 1;
                    if (localTiles.Contains(neighbor)) // Save vector only for this chunk
                    {
                        flowField[neighbor] = (GridToWorld(current) - GridToWorld(neighbor)).Normalized();
                        DetailedFlowFields[neighbor] = (GridToWorld(current) - GridToWorld(neighbor)).Normalized();
                    }
                    openQueue.Enqueue(neighbor);
                }
            }
        }
        _detailedFlowFields[chunkCoord] = flowField;
    }

    public void GenerateLowResField(Vector2I chunkCoor, Vector2I playerChunkCoor)
    {
        Vector2 chunkWorldCenter = ChunkManager.GetChunkCenter(chunkCoor);
        Vector2 playerChunkCenter = ChunkManager.GetChunkCenter(playerChunkCoor);
        Vector2 direction = (playerChunkCenter - chunkWorldCenter).Normalized();
         
        _chunkDirectionMap[chunkCoor] = direction;
    }

    private Vector2 GridToWorld(Vector2I gridPos)
    {
        return new Vector2(
            gridPos.X * tileSize + tileSize / 2f,
            gridPos.Y * tileSize + tileSize / 2f
        );
    }

    private IEnumerable<Vector2I> GetWalkableNeighbors(Vector2I cell)
    {
        // This should be replaced with actual checks from your world data (e.g., TileMap or ChunkManager)
        Vector2I[] directions = new Vector2I[]
        {
            new(1, 0), new(-1, 0), new(0, 1), new(0, -1)
        };

        foreach (Vector2I dir in directions)
        {
            Vector2I neighbor = cell + dir;
            if (IsWalkable(neighbor))
                yield return neighbor;
        }
    }

    //TODO: Validate the walkable paths.
    private bool IsWalkable(Vector2I gridPos)
    {
        return true;
    }
}
