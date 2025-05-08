using Godot;
using System.Collections.Generic;

public partial class FlowFieldManager : Node2D
{
    [Export]
    public ChunkManager ChunkManager { get; set; }
    public Dictionary<Vector2I, Vector2> ChunkDirectionMap = new();
    public Dictionary<Vector2I, Vector2> DetailedFlowFields = new();
    public int tileSize = 64;

    private Dictionary<Vector2I, Vector2> _flowChart = new();
    private Dictionary<Vector2I, int> _costField = new();

    public override void _Ready()
    {
       GodotUtilities.RegisterFlowField(this); 
    }

    public void GenerateHighResField(Vector2I chunkCoord, Vector2 playerPos)
    {
        DetailedFlowFields.Clear();
        var flowField = new Dictionary<Vector2I, Vector2>();
        Vector2I goalGrid = ChunkManager.TileMap.LocalToMap(playerPos);
         
        List<Vector2I> localTiles = ChunkManager.GetTilesInChunk(chunkCoord);

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
                    DetailedFlowFields[neighbor] = (GridToWorld(current) - GridToWorld(neighbor)).Normalized();
                    openQueue.Enqueue(neighbor);
                }
            }
        }
    }

    public void GenerateLowResField(Vector2I chunkCoor, Vector2I playerChunkCoor)
    {
        Vector2 chunkWorldCenter = ChunkManager.GetChunkCenter(chunkCoor);
        Vector2 playerChunkCenter = ChunkManager.GetChunkCenter(playerChunkCoor);
        Vector2 direction = (playerChunkCenter - chunkWorldCenter).Normalized();
         
        ChunkDirectionMap[chunkCoor] = direction;
    }
    
    public Vector2 GetFlowVector(Vector2 gloabalPos)
    {
        Vector2I gridPos = ChunkManager.TileMap.LocalToMap(gloabalPos);
        Vector2I chunkPos = ChunkManager.GetCurrentChunk(gloabalPos);

        if (ChunkDirectionMap.TryGetValue(chunkPos, out var chunkDir))
        {
            return chunkDir;
        }

        if (DetailedFlowFields.TryGetValue(gridPos, out var dir))
        {
            return dir;
        }

        return Vector2.Zero;
            
    }

    private Vector2I WorldToGrid(Vector2 worldPos)
    {
        return new Vector2I(Mathf.FloorToInt(worldPos.X / tileSize), Mathf.FloorToInt(worldPos.Y / tileSize));
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
            Vector2I.Up, Vector2I.Down, Vector2I.Left, Vector2I.Right,
            new(-1, -1), new(-1, 1), new(1, -1),new(1, 1),
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

    public void ValidateChunkMapWithCurrent(Vector2I currentChunk)
    {
        if (ChunkDirectionMap.ContainsKey(currentChunk))
        {
            ChunkDirectionMap.Remove(currentChunk);
        }
    }
        
}
