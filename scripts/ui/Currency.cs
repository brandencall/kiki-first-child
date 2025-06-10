using Godot;

public partial class Currency : Control
{
	[Export]
	public Label CurrentCurrencyLabel { get; set; }

	private GameManager GameManager => GetNode<GameManager>("/root/GameManager"); 

	public override void _Ready()
	{
		CurrentCurrencyLabel.Text = GameManager.GameStateData.Schmeckels.ToString();
		GameManager.CurrencyChanged += SetCurrency;
	}

	private void SetCurrency(int currency)
	{
		CurrentCurrencyLabel.Text = currency.ToString();
	}
}
