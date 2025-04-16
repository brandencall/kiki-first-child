using Godot;

public partial class ExperienceManager : Node
{
    [Signal]
    public delegate void LevelIncreaseEventHandler();

    public int CurrentExperienceLevel { get; private set; }
    public float CurrentExperience { get; private set; }
    public float MaxExperienceForLevel { get; set; }

    public override void _Ready()
    {
        CurrentExperienceLevel = 1;
        CurrentExperience = 0;
        MaxExperienceForLevel = 10;
    }

    public void IncreaseExperience(float experience)
    {
        CurrentExperience += experience;
        if (CurrentExperience >= MaxExperienceForLevel)
        {
            NewExperienceLevel();
        }
    }

    public void NewExperienceLevel()
    {
        CurrentExperienceLevel++;
        float remainingExperience = CurrentExperience - MaxExperienceForLevel;
        SetNewMaxExperienceForLevel();
        CurrentExperience = remainingExperience;
        EmitSignal(SignalName.LevelIncrease);
    }

    private void SetNewMaxExperienceForLevel()
    {
        MaxExperienceForLevel = MaxExperienceForLevel * 2;
    }

}
