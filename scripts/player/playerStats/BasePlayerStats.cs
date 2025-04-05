using Godot;

[GlobalClass]
public partial class BasePlayerStats : Resource {

    [Export]
    public int Health { get; set; }
    [Export]
    public int Speed { get; set; }

    public BasePlayerStats() : this(0,0){}

    public BasePlayerStats(int health, int speed)
    {
        Health = health;
        Speed = speed;
    }
}
