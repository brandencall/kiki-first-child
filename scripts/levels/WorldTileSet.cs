using Godot;
using System.Collections.Generic;

public partial class WorldTileSet : TileMapLayer
{
    private FastNoiseLite _noise = new FastNoiseLite();
    private int _width = 8;
    private int _height = 8;
    [Export]
    public BasePlayer Player { get; set; }

    public List<Vector2I> chunks = new();

    public override void _Ready()
    {
        _noise.Seed = (int)GD.Randi();
        Player.VelocityComponent.MaxSpeed *= 2;
    }

    public override void _Process(double delta)
    {
        var tilePosition = LocalToMap(Player.Position);
        GenerateChunk(tilePosition);
        UnloadDistantChunks(tilePosition);
    }

    private void GenerateChunk(Vector2I position)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var mapX = position.X + x - _width / 2;
                var mapY = position.Y + y - _height / 2;

                SetCell(new Vector2I(mapX, mapY), 0, new Vector2I(1, 1), 0);
            }
        }

        if (!chunks.Contains(position))
        {
            chunks.Add(position);
        }
    }

    private void UnloadDistantChunks(Vector2I playerPos)
    {
        int threshold = (_width * 2) + 1;

        for (int i = chunks.Count - 1; i >= 0; i--)
        {
            Vector2I chunk = chunks[i];
            float dist = GetDist(chunk, playerPos);

            if (dist > threshold)
            {
                GD.Print(chunks[i]);
                ClearChunk(chunk);
                chunks.RemoveAt(i);
            }
        }
    }

    private void ClearChunk(Vector2I pos)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2I tilePos = new(pos.X - (_width / 2) + x, pos.Y - (_height / 2) + y);
                EraseCell(tilePos);
            }
        }
    }

    private float GetDist(Vector2I p1, Vector2I p2)
    {
        Vector2I diff = p1 - p2;
        return Mathf.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
    }

}
