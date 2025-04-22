using Godot;
using System.Collections.Generic;

public partial class ChunkManager : Node2D
{
    [Export]
    public BasePlayer Player { get; set; }
    [Export]
    public WorldTileSet TileMap { get; set; }
    [Export]
    public int chunkSize = 8;
    private int tileSize = 64;
    [Export]
    public int renderDistance = 2;

    private Vector2I currentChunk;
    private Vector2I previousChunk;
    private List<Vector2I> activeChunks = new();

    public override void _Ready()
    {
        currentChunk = GetCurrentChunk(Player.GlobalPosition);
        activeChunks.Add(currentChunk);
        TileMap.Generate(currentChunk, chunkSize);
        RenderChunk();
    }

    public override void _PhysicsProcess(double delta)
    {
        currentChunk = GetCurrentChunk(Player.GlobalPosition);
        if (currentChunk != previousChunk)
        {
            RenderChunk();
        }

        previousChunk = currentChunk;
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
        List<Vector2I> newChunks = new();

        for (int x = 0; x < totalChunksToRender; x++)
        {
            int chunkX = renderDistance + currentChunk.X - x;
            int chunkPositionX = chunkX * chunkSize;
            for (int y = 0; y < totalChunksToRender; y++)
            {
                int chunkY = renderDistance + currentChunk.Y - y;
                int chunkPositionY = chunkY * chunkSize;

                Vector2I chunk = new Vector2I(chunkX, chunkY);
                newChunks.Add(chunk);
                if (!activeChunks.Contains(chunk))
                {
                    TileMap.Generate(new Vector2I(chunkPositionX, chunkPositionY), chunkSize);
                    activeChunks.Add(chunk);
                }
            }
        }

        List<Vector2I> chunksToDelete = new();
        for (int i = 0; i < activeChunks.Count; i++)
        {
            var chunk = activeChunks[i];
            if (Mathf.Abs(chunk.X - currentChunk.X) > renderDistance
                        || Mathf.Abs(chunk.Y - currentChunk.Y) > renderDistance)
            {
                Vector2I chunkToRemove = new Vector2I(chunk.X * chunkSize, chunk.Y * chunkSize);
                TileMap.ClearChunk(chunkToRemove, chunkSize);
                chunksToDelete.Add(chunk);
            }
        }

        foreach (var chunk in chunksToDelete)
        {
            activeChunks.Remove(chunk);
        }
    }
}
