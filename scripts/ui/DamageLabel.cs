using Godot;

public partial class DamageLabel : Node2D
{
	[Export]
	private Label _damageLabel;
	[Export]
	private float _riseDistance = 30f;
	[Export]
	private float _duration = 0.75f;

	public void Initialize(float amount)
	{
		_damageLabel.Text = amount.ToString("0");
		StartTween();
	}

	private void StartTween()
	{
		var tween = CreateTween();
		tween.SetEase(Tween.EaseType.Out);
		tween.SetTrans(Tween.TransitionType.Quad);

		// Move upward smoothly
		tween.TweenProperty(this, "position:y", Position.Y - _riseDistance, _duration);

		// Fade out at the same time
		tween.TweenProperty(_damageLabel, "modulate:a", 0f, _duration);
		tween.Finished += () => QueueFree();
	}
}
