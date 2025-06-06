using Godot;

public partial class Tonts : BaseCharacter
{
	[Export]
	public PackedScene BulletScene;
	[Export]
	public Marker2D Mussel;

	public override void OnBaseAttackTimerTimeout()
	{
		base.OnBaseAttackTimerTimeout();
		HitboxComponent bullet = BulletScene.Instantiate<HitboxComponent>();
		GetTree().Root.AddChild(bullet);
		SetMusselPosition();
		bullet.GlobalPosition = Mussel.GlobalPosition;
	}

	private void SetMusselPosition()
	{
		if (VelocityComponent.LastMoveDirection.X > 0)
		{
			Vector2 position = new Vector2(Mathf.Abs(Mussel.Position.X), Mussel.Position.Y);
			Mussel.Position = position;
		}
		else if (VelocityComponent.LastMoveDirection.X < 0)
		{
			Vector2 position = new Vector2(-Mathf.Abs(Mussel.Position.X), Mussel.Position.Y);
			Mussel.Position = position;
		}
	}
}
