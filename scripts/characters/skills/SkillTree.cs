using Godot;
using System.Collections.Generic;

public class SkillTree
{
    private BaseCharacter _character;
    private List<SkillData> _skill;

    public SkillTree(List<SkillData> skills, BaseCharacter character)
    {
        _skill = skills;
        _character = character;
        ParseAndApplySkills();
    }

    private void ParseAndApplySkills()
    {
        if (_skill == null) return;

        foreach (var skill in _skill)
        {
            if (skill.IsUnlocked == true)
            {
                PackedScene skillScene = GD.Load<PackedScene>(skill.ScenePath);
                ISkill skillNode = skillScene.Instantiate<ISkill>();
                skillNode.Apply(_character);
            }
        }
    }
}
