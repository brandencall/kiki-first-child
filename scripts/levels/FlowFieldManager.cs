using Godot;
using System.Collections.Generic;

public partial class FlowFieldManager : Node2D
{
    [Export]
    public ChunkManager ChunkManager { get; set; }
    [Export]
    public Timer LowResUpdateTimer { get; set; }

    /// <summary>
    /// Per-chunk low-resolution flow directions (world-space, normalized).
    /// </summary>
    public Dictionary<Vector2I, Vector2> ChunkDirectionMap = new();

    /// <summary>
    /// Per-tile high-resolution flow directions inside the player's current chunk.
    /// </summary>
    public Dictionary<Vector2I, Vector2> DetailedFlowFields = new();

    public int tileSize = 64;

    // How many tiles from a chunk edge are considered the "blend zone"
    // for edge normalization between low-res and high-res vectors.
    private const int EdgeBlendTiles = 2;

    public override void _Ready()
    {
        GodotUtilities.RegisterFlowField(this);
        LowResUpdateTimer.Timeout += LowResTimeout;
    }

    // -------------------------------------------------------------------------
    // High-res (per-tile BFS) field for the player's current chunk
    // -------------------------------------------------------------------------

    public void GenerateHighResField(Vector2I chunkCoord, Vector2 playerPos)
    {
        DetailedFlowFields.Clear();

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
                    continue;

                if (!costField.ContainsKey(neighbor) || costField[neighbor] > currentCost + 1)
                {
                    costField[neighbor] = currentCost + 1;

                    // Direction from neighbor toward the goal (current cell is one step closer).
                    Vector2 neighborVector = (GridToWorld(current) - GridToWorld(neighbor)).Normalized();

                    // Smooth adjacent vectors with a lerp to reduce sharp turns.
                    const float BlendWeight = 0.5f;
                    DetailedFlowFields[neighbor] = DetailedFlowFields.TryGetValue(current, out var parentVec)
                        ? parentVec.Lerp(neighborVector, BlendWeight).Normalized()
                        : neighborVector;

                    openQueue.Enqueue(neighbor);
                }
            }
        }
    }

    // -------------------------------------------------------------------------
    // Low-res (per-chunk) field
    // -------------------------------------------------------------------------

    /// <summary>
    /// Generates a low-res direction for <paramref name="chunkCoor"/> pointing
    /// toward the player's world position. Skips the player's current chunk
    /// because that is covered by the high-res field.
    /// </summary>
    public void GenerateLowResField(Vector2I chunkCoor, Vector2 playerPos, Vector2I currentChunk)
    {
        if (chunkCoor == currentChunk)
            return;

        // Both values are now consistently in world space.
        Vector2 chunkWorldCenter = ChunkManager.GetChunkCenter(chunkCoor);
        Vector2 direction = (playerPos - chunkWorldCenter).Normalized();
        ChunkDirectionMap[chunkCoor] = direction;
    }

    /// <summary>
    /// Refreshes every cached low-res direction to point at the latest player position.
    /// Called on a timer or whenever the player moves within their current chunk.
    /// </summary>
    public void UpdateLowResField(Vector2 playerPos)
    {
        // Iterate over a snapshot of the keys so we can safely update values.
        var keys = new List<Vector2I>(ChunkDirectionMap.Keys);
        foreach (var chunkCoor in keys)
        {
            // Both values are in world space — direction math is now correct.
            Vector2 chunkWorldCenter = ChunkManager.GetChunkCenter(chunkCoor);
            ChunkDirectionMap[chunkCoor] = (playerPos - chunkWorldCenter).Normalized();
        }
    }

    private void LowResTimeout()
    {
        UpdateLowResField(ChunkManager.Character.GlobalPosition);
    }

    // -------------------------------------------------------------------------
    // Public flow-vector query with edge blending
    // -------------------------------------------------------------------------

    /// <summary>
    /// Returns the flow vector for any world-space position.
    /// Tiles near the boundary of the player's high-res chunk are blended
    /// with the neighbouring chunk's low-res vector so agents transition
    /// smoothly rather than snapping direction at the chunk edge.
    /// </summary>
    public Vector2 GetFlowVector(Vector2 globalPos)
    {
        Vector2I gridPos = ChunkManager.TileMap.LocalToMap(globalPos);
        Vector2I chunkPos = ChunkManager.GetCurrentChunk(globalPos);

        bool hasHighRes = DetailedFlowFields.TryGetValue(gridPos, out var highResDir);
        bool hasLowRes = ChunkDirectionMap.TryGetValue(chunkPos, out var lowResDir);

        // No data at all — return zero and let callers handle it.
        if (!hasHighRes && !hasLowRes)
            return Vector2.Zero;

        // Only one source available — return it directly.
        if (!hasHighRes) return lowResDir;
        if (!hasLowRes) return highResDir;

        // Both available: blend at chunk edges.
        float edgeWeight = ComputeEdgeBlendWeight(gridPos, chunkPos);
        return highResDir.Lerp(lowResDir, edgeWeight).Normalized();
    }

    /// <summary>
    /// Returns a blend weight (0 = fully high-res, 1 = fully low-res) based on
    /// how close <paramref name="gridPos"/> is to the edge of its chunk.
    /// Tiles within <see cref="EdgeBlendTiles"/> of any edge ramp up to 1.
    /// </summary>
    private float ComputeEdgeBlendWeight(Vector2I gridPos, Vector2I chunkPos)
    {
        // Find the tile-space origin of this chunk.
        int chunkOriginX = chunkPos.X * ChunkManager.chunkSize;
        int chunkOriginY = chunkPos.Y * ChunkManager.chunkSize;

        // Distance (in tiles) from each edge of the chunk.
        int distLeft = gridPos.X - chunkOriginX;
        int distTop = gridPos.Y - chunkOriginY;
        int distRight = (ChunkManager.chunkSize - 1) - distLeft;
        int distBottom = (ChunkManager.chunkSize - 1) - distTop;

        int minDist = Mathf.Min(Mathf.Min(distLeft, distRight), Mathf.Min(distTop, distBottom));

        if (minDist >= EdgeBlendTiles)
            return 0f; // Well inside the chunk — use high-res only.

        // Linearly ramp from 0 (at EdgeBlendTiles away) to 1 (at the edge).
        return 1f - (minDist / (float)EdgeBlendTiles);
    }

    // -------------------------------------------------------------------------
    // Chunk lifecycle
    // -------------------------------------------------------------------------

    /// <summary>
    /// Removes the player's current chunk from the low-res map so it is not
    /// used for steering while the high-res field is active there.
    /// </summary>
    public void ValidateChunkMapWithCurrent(Vector2I currentChunk)
    {
        ChunkDirectionMap.Remove(currentChunk);
    }

    // -------------------------------------------------------------------------
    // Grid / world helpers
    // -------------------------------------------------------------------------

    private Vector2I WorldToGrid(Vector2 worldPos)
    {
        return new Vector2I(
            Mathf.FloorToInt(worldPos.X / tileSize),
            Mathf.FloorToInt(worldPos.Y / tileSize));
    }

    private Vector2 GridToWorld(Vector2I gridPos)
    {
        return new Vector2(
            gridPos.X * tileSize + tileSize / 2f,
            gridPos.Y * tileSize + tileSize / 2f);
    }

    // -------------------------------------------------------------------------
    // Neighbor / walkability helpers
    // -------------------------------------------------------------------------

    private IEnumerable<Vector2I> GetWalkableNeighbors(Vector2I cell)
    {
        Vector2I[] directions =
        {
            Vector2I.Up, Vector2I.Down, Vector2I.Left, Vector2I.Right,
            new(-1, -1), new(-1, 1), new(1, -1), new(1, 1),
        };

        foreach (Vector2I dir in directions)
        {
            Vector2I neighbor = cell + dir;
            if (IsWalkable(neighbor))
                yield return neighbor;
        }
    }

    // TODO: Replace with real obstacle data from TileMap / ChunkManager.
    private bool IsWalkable(Vector2I gridPos)
    {
        return true;
    }
}
