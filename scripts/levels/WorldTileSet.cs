using Godot;
using System.Collections.Generic;

public partial class WorldTileSet : TileMapLayer
{
    private FastNoiseLite _noise = new FastNoiseLite();
    private int _width = 8;
    private int _height = 8;

    public HashSet<Vector2I> chunks = new();

    public override void _Ready()
    {

        _noise.Seed = (int)GD.Randi();
    }

    public void Generate(Vector2I position, int chunkSize)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                var mapX = position.X + x;
                var mapY = position.Y + y;
                SetCell(new Vector2I(mapX, mapY), 0, new Vector2I(1, 1), 0);
            }
        }
    }

    public void ClearChunk(Vector2I pos, int chunkSize)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector2I tilePos = new(pos.X + x, pos.Y + y);
                EraseCell(tilePos);
            }
        }
    }

    public List<Vector2I> GetTilesInChunk(Vector2I chunkOrigin, int chunkSize)
    {
        var tiles = new List<Vector2I>();

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                var mapX = chunkOrigin.X + x;
                var mapY = chunkOrigin.Y + y;
                tiles.Add(new Vector2I(mapX, mapY));
            }
        }
        return tiles;
    }

}
