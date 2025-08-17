using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public List<CharacterData> Characters { get; set; }
    public List<LevelData> Levels { get; set; }
    public CharacterData LastUsedCharacter { get; set; }
    public int Schmeckels { get; set; } = 0;
}

