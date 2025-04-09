using Godot;

public static class GodotUtilities 
{
    public static CharacterBody2D GetPlayer(this Node node)
    {
        var players = node.GetTree().GetNodesInGroup("player");
        return players.Count > 0 ? players[0] as CharacterBody2D : null;
    }
}
