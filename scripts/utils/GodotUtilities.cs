using Godot;

public static class GodotUtilities 
{
    private static BasePlayer _cachedPlayer;
    private static FlowFieldManager _flowField;

    public static void RegisterPlayer(BasePlayer player)
    {
        _cachedPlayer = player;
    }

    public static void UnregisterPlayer(BasePlayer player)
    {
        _cachedPlayer = null;
    }

    public static BasePlayer GetPlayer()
    {
        return _cachedPlayer;
    }

    public static BasePlayer GetPlayer(this Node node)
    {
        return GetPlayer();
    }

    public static void RegisterFlowField(FlowFieldManager flowField)
    {
        _flowField = flowField;
    }

    public static FlowFieldManager GetFlowField(this Node node)
    {
        return _flowField;
    }
}
