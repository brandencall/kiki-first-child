using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public List<CharacterData> Characters { get; set; }
    public List<WeaponData> Weapons { get; set; }
}

