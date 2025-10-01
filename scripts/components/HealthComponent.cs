using Godot;

public partial class HealthComponent : Node2D
{
	[Signal]
	public delegate void HealthChangedEventHandler(float healthUpdate);
	[Signal]
	public delegate void DiedEventHandler();
	[Signal]
	public delegate void MaxHealthChangedEventHandler(float maxHealth);

	public bool HasHealthRemaining => !Mathf.IsEqualApprox(CurrentHealth, 0f);

	[Export]
	public float MaxHealth
	{
		get => _maxHealth;
		private set
		{
			_maxHealth = value;
			if (CurrentHealth > _maxHealth)
			{
				CurrentHealth = _maxHealth;
			}
		}
	}

	public float CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			_currentHealth = Mathf.Clamp(value, 0, MaxHealth);
			EmitSignal(SignalName.HealthChanged, _currentHealth);
			if (!HasHealthRemaining && !_hasDied)
			{
				_hasDied = true;
				EmitSignal(SignalName.Died);
			}
		}
	}

	private bool _hasDied;
	private float _maxHealth;
	private float _currentHealth;

	public override void _Ready()
	{
		CallDeferred(nameof(IntializeHealth));
	}

	private void IntializeHealth()
	{
		CurrentHealth = MaxHealth;
		_maxHealth = MaxHealth;
		_currentHealth = CurrentHealth;
	}

	public void SetMaxHealth(float health)
	{
	   MaxHealth = health; 
	   EmitSignal(SignalName.MaxHealthChanged, health);
	}

	public void Damage(float damage)
	{
		CurrentHealth -= damage;
	}

	public void ResetHealth()
	{
		CurrentHealth = MaxHealth;
	}
}
