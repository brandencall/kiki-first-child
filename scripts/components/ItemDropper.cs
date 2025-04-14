using Godot;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

public partial class ItemDropper : Node2D
{
    private Random _random = new Random();
    private List<DropItemEntry> _droppableItems;
    private string _itemConfiguration = "res://Data/droppableItems.json";

    public override void _Ready()
    {
        var jsonFile = FileAccess.Open(_itemConfiguration, FileAccess.ModeFlags.Read);
        string jsonText = jsonFile.GetAsText();
        _droppableItems = JsonConvert.DeserializeObject<List<DropItemEntry>>(jsonText);
    }

    public void DropItem()
    {
        DropItemEntry item = GetRandomItem();
        if (item.Scene == null) return;

        PackedScene itemScene = GD.Load<PackedScene>(item.Scene);
        Area2D itemNode = (Area2D)itemScene.Instantiate();
        itemNode.GlobalPosition = GlobalPosition;
        GetTree().Root.CallDeferred("add_child", itemNode);
    }

    private DropItemEntry GetRandomItem()
    {
        float roll = (float)_random.NextDouble();
        float cumulative = 0.0f;

        foreach (var item in _droppableItems)
        {
            cumulative += item.Probability;
            if (roll <= cumulative)
            {
                return item;
            }
        }

        return _droppableItems[0];
    }
}
