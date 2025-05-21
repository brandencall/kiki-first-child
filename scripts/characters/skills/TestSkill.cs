using Godot;
using System;

public partial class TestSkill : Node, ISkill
{
	public void Apply(BaseCharacter character)
	{
		GD.Print("Health: " + character.HealthComponent.MaxHealth);
		character.HealthComponent.SetMaxHealth(200);
		GD.Print("Health after skill applied: " + character.HealthComponent.MaxHealth);
	}
}
