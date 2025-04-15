using Godot;

public partial class Hud : Control
{
    [Export]
    public Label ScoreLable { get; set; }

    private float _currentScore = 0;

    public override void _Ready()
    {
        ScoreLable.Text = _currentScore.ToString();
    }

    public void IncreaseExperience(float experience)
    {
        _currentScore += experience;
        ScoreLable.Text = _currentScore.ToString();
    }
}
