using Godot;

public partial class Hud : Control
{
    [Export]
    public ProgressBar ExperienceBar { get; set; }

    private float _currentScore = 0;

    // TODO: create an experience object for tracking the different variables.
    private float _maxValue = 10;
    private float _currentValue = 0;

    public override void _Ready()
    {
        ExperienceBar.Value = _currentValue;
        ExperienceBar.MaxValue = _maxValue;
    }

    public void IncreaseExperience(float experience)
    {
        _currentValue += experience;
        ExperienceBar.Value = _currentValue;
    }
}
