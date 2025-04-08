using Godot;

public partial class Enemy : CharacterBody2D
{

    [Export]
    public Hitbox Hitbox { get; set; }
    [Export]
    protected int speed = 200;
    [Export]
    protected int damage = 10;

    private Vector2 _playerPosition;
    private Player _player;

    public void Initialize(Player player)
    {
        _player = player;
    }

    public override void _Ready()
    {
        if (_player != null)
        {
            _player.PlayerMoved += UpdatePlayerPosition;
        }
        Hitbox.TakeDamage += OnTakeDamage;
    }

    private void UpdatePlayerPosition(Vector2 newPosition)
    {
        _playerPosition = newPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 newPosition = Position.MoveToward(_playerPosition, speed * (float)delta); 
        Position = newPosition;
    }

    public void OnAttackAreaBodyEntered(Node2D body)
    {
        if (body == _player)
        {
            Attack();
        }
    }
        
    public void Attack()
    {
        _player.TakeDamage(damage);
    }

    public void OnTakeDamage(int damage)
    {
    }
}
