using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

public partial class AbilityManager : Node
{
    public int NumberOfAbilitiesToChoose { get; private set; } = 3;
    public List<IAbility> CurrentAbilities = new();
    private List<IAbility> _allAbilities = new();

    public override void _Ready()
    {
        LoadAllIAbilities();
    }

    private void LoadAllIAbilities()
    {
        _allAbilities.Add(new SoundPulse());
        _allAbilities.Add(new SpeedBoost());
        _allAbilities.Add(new Football());
    }

    public List<IAbility> GetRandomAbilities()
    {
        Random random = new Random();
        List<IAbility> result = _allAbilities.OrderBy(_ => random.Next())
                                       .Take(NumberOfAbilitiesToChoose).ToList();
        return result;
    }
}
