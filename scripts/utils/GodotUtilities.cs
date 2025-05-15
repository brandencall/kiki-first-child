using Godot;

public static class GodotUtilities 
{
    private static BaseCharacter _cachedCharacter;
    private static FlowFieldManager _flowField;

    public static void RegisterCharacter(BaseCharacter character)
    {
        _cachedCharacter = character;
    }

    public static void UnregisterCharacter(BaseCharacter character)
    {
        _cachedCharacter = null;
    }

    public static BaseCharacter GetCharacter()
    {
        return _cachedCharacter;
    }

    public static BaseCharacter GetCharacter(this Node node)
    {
        return GetCharacter();
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
