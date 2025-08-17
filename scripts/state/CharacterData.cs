using System;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string Id { get; set; }
    public bool IsUnlocked { get; set; }
    public string Scene { get; set; }
    public string IconPath { get; set; }
    public int Cost { get; set; }
    public List<SkillData> Skills { get; set; }
}
