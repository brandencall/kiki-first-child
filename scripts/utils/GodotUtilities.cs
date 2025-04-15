using Godot;

public static class GodotUtilities 
{
    private static BasePlayer _cachedPlayer;

    public static void RegisterPlayer(BasePlayer player)
    {
        _cachedPlayer = player;
    }

    public static BasePlayer GetPlayer()
    {
        return _cachedPlayer;
    }

    public static BasePlayer GetPlayer(this Node node)
    {
        return GetPlayer();
    }
}
