using System;
using System.Collections.Generic;

[Serializable]
public class SkillData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string IconPath { get; set; }
    public string ScenePath { get; set; }
    public int Cost { get; set; }
    public List<string> Prerequisites { get; set; } = new();
    public bool IsUnlocked { get; set; }
}
