using Godot;

public partial class BackwardsNoodleSkill : Node2D, ISkill
{
	[Export]
	public Noodle Noodle { get; set; }

	public void Apply(BaseCharacter character)
	{
		character.AddChild(this);
		Noodle.Initialize(character, character.BaseAttackDamage);
		character.Attack += Attack;
	}

	private void Attack()
	{
		Noodle.AttackBackwards();
	}
}
