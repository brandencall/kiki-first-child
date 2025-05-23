using Godot;

public class SkillTree
{
    private SkillTreeData _skillTreeData;
    private BaseCharacter _character;

    public SkillTree(SkillTreeData skillTreeData, BaseCharacter character)
    {
        _skillTreeData = skillTreeData;
        _character = character;
        ParseAndApplySkills();
    }

    private void ParseAndApplySkills()
    {
        if (_skillTreeData == null) return;
        if (_skillTreeData.Skills == null) return;

        foreach (var skill in _skillTreeData.Skills)
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
