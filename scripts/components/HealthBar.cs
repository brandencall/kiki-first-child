using Godot;

public partial class HealthBar : ProgressBar
{
	[Export]
	private HealthComponent _healthComponent;
	private Timer _timer;
	private ProgressBar _damageBar;
	private float _health;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_damageBar = GetNode<ProgressBar>("DamageBar");
		_health = _healthComponent.MaxHealth;
		MaxValue = _health;
		Value = _health;
		_damageBar.MaxValue = _health;
		_damageBar.Value = _health;
		_healthComponent.HealthChanged += HealthChanged;
		_healthComponent.MaxHealthChanged += MaxHealthChanged;
	}

	private void HealthChanged(float newHealth)
	{
		float previousHealth = _health;
		_health = newHealth;
		Value = _health;

		if (_health < previousHealth)
		{
			_timer.Start();
		}
		else
		{
			_damageBar.Value = _health;
		}
	}

	private void MaxHealthChanged(float maxHealth)
	{
		MaxValue = maxHealth;
		_damageBar.MaxValue = maxHealth;
	}

	private void OnTimerTimeout()
	{
		_damageBar.Value = _health;
	}

}
