using System;

[Serializable]
public class CharacterData
{
    public string Id { get; set; }
    public bool IsUnlocked { get; set; }
    public string Scene { get; set; }
    public string IconPath { get; set; }
}
