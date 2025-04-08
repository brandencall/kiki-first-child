using Godot;

[GlobalClass]
public partial class BasePlayerStats : Resource {

    [Export]
    public int Health { get; set; }
    [Export]
    public int Speed { get; set; }
    [Export]
    public int Damage { get; set; }

    public BasePlayerStats() : this(0,0,0){}

    public BasePlayerStats(int health, int speed, int damage)
    {
        Health = health;
        Speed = speed;
        Damage = damage;
    }
}
