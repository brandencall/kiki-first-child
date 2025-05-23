using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
    public BaseCharacter CurrentCharacter { get; set; }
    public List<BaseEnemy> CurrentEnemies { get; set; }
    public string CurrentScene { get; set; }
}
