using System;
using System.Collections.Generic;

[Serializable]
public class SkillTreeData
{
    public string CharacterId { get; set; }
    public List<SkillData> Skills { get; set; } = new();
}
