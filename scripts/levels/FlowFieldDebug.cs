using Godot;
using System.Collections.Generic;

public partial class FlowFieldDebug : Node2D
{
    public Dictionary<Vector2I, Vector2> FlowVectors = new();
    [Export]
    public ChunkManager ChunkManager { get; set; }
    [Export]
    public WorldTileSet TileMap { get; set; }
    public float ArrowLength = 16f;

    public override void _Draw()
    {
        //DrawLowResFlowVectors();
        DrawHighResFlowVectors();
    }

    private void DrawLowResFlowVectors()
    {
        foreach (var pair in FlowVectors)
        {
            Vector2I chunkPos = pair.Key;
            List<Vector2I> tilePositions = ChunkManager.GetTilesInChunk(chunkPos);
            foreach (var tile in tilePositions)
            {
                Vector2 worldPos = TileMap.MapToLocal(tile);
                Vector2 dir = pair.Value;
                if (dir == Vector2.Zero)
                    continue;
                Vector2 endPos = worldPos + dir.Normalized() * ArrowLength;

                DrawLine(worldPos, endPos, Colors.Red, 2);

                // Optional: draw arrowhead
                Vector2 perp = dir.Normalized().Orthogonal() * 4;
                Vector2 tip1 = endPos - dir.Normalized() * 6 + perp;
                Vector2 tip2 = endPos - dir.Normalized() * 6 - perp;

                DrawLine(endPos, tip1, Colors.Red, 2);
                DrawLine(endPos, tip2, Colors.Red, 2);
            }
        }
    }

    private void DrawHighResFlowVectors()
    {
        foreach (var tile in FlowVectors)
        {
            Vector2 worldPos = TileMap.MapToLocal(tile.Key);
            Vector2 dir = tile.Value;
            if (dir == Vector2.Zero)
                continue;

            Vector2 endPos = worldPos + dir.Normalized() * ArrowLength;

            DrawLine(worldPos, endPos, Colors.Red, 2);
            // Optional: draw arrowhead
            Vector2 perp = dir.Normalized().Orthogonal() * 4;
            Vector2 tip1 = endPos - dir.Normalized() * 6 + perp;
            Vector2 tip2 = endPos - dir.Normalized() * 6 - perp;

            DrawLine(endPos, tip1, Colors.Red, 2);
            DrawLine(endPos, tip2, Colors.Red, 2);
        }
    }

    public void SetFlowVectors(Dictionary<Vector2I, Vector2> vectors)
    {
        FlowVectors = vectors;
        QueueRedraw(); // Forces _Draw to update
    }
}

