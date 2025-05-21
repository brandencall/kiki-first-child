using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public List<CharacterData> Characters { get; set; }
    public List<LevelData> Levels { get; set; }
    public List<SkillTreeData> SkillTrees { get; set; }
    public CharacterData LastUsedCharacter { get; set; }
    //Currency for unlocking characters or skills
    public int Schmeckels { get; set; } = 0;
}

