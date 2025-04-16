using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

public partial class AbilityManager : Node
{
    public int NumberOfAbilitiesToChoose { get; private set; } = 3;
    private string _abilityDirectory = "res://data/abilities";
    private List<AbilityResource> _allAbilities = new();

    public override void _Ready()
    {
        LoadAllAbilities();
    }

    public void LoadAllAbilities()
    {
        var dir = DirAccess.Open(_abilityDirectory);
        if (dir == null) return;

        dir.ListDirBegin();
        string fileName;
        while ((fileName = dir.GetNext()) != "")
        {
            if (!fileName.EndsWith(".tres")) continue;
            var ability = GD.Load<AbilityResource>(_abilityDirectory + "/" + fileName);
            if (ability != null)
                _allAbilities.Add(ability);
        }
        dir.ListDirEnd();
    }

    public List<AbilityResource> GetRandomAbilities()
    {
        Random random = new Random();
        List<AbilityResource> result = _allAbilities.OrderBy(_ => random.Next())
                                       .Take(NumberOfAbilitiesToChoose).ToList();
        return result;
    }
}
