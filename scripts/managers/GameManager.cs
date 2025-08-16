using Godot;
using System;

public partial class GameManager : Node
{
	public BaseCharacter CurrentCharacter { get; set; }
	public string CurrentScene { get; set; }

	private GameState _gameStateData;
	public GameState GameStateData 
	{ 
		get => _gameStateData; 
		set => _gameStateData = value; 
	}

	public event Action<int> CurrencyChanged;

	public void Initialize(GameState gameStateData)
	{
		_gameStateData = gameStateData;

		if (_gameStateData.LastUsedCharacter != null)
		{
			CharacterData currentCharacterData = _gameStateData.LastUsedCharacter;
			PackedScene characterScene = GD.Load<PackedScene>(currentCharacterData.Scene);
			CurrentCharacter = characterScene.Instantiate<BaseCharacter>();
			CurrentCharacter.Initialize(currentCharacterData);
		}
		else
		{
			CharacterData currentCharacterData = _gameStateData.Characters[0];
			PackedScene characterScene = GD.Load<PackedScene>(currentCharacterData.Scene);
			CurrentCharacter = characterScene.Instantiate<BaseCharacter>();
			CurrentCharacter.Initialize(currentCharacterData);
		}
	}

	public void UnlockCharacter(string characterId)
	{
		CharacterData character = GameStateData.Characters.Find(c => c.Id == characterId);
		if (character != null && !character.IsUnlocked)
		{
			character.IsUnlocked = true;
		}
	}

	public void CharacterDied(BaseCharacter character)
	{
		CharacterData currentCharacterData = character.CharacterData;
		PackedScene characterScene = GD.Load<PackedScene>(currentCharacterData.Scene);
		CurrentCharacter = characterScene.Instantiate<BaseCharacter>();
		CurrentCharacter.Initialize(currentCharacterData);
	}

	public void DepositCurrenct(int currencyToDeposit)
	{
		_gameStateData.Schmeckels += currencyToDeposit;
		CurrencyChanged?.Invoke(GameStateData.Schmeckels);
	}

	public int CurrencyBalance()
	{
		return _gameStateData.Schmeckels;
	}

	public void WithdrawCurrency(int currencyToWithdraw)
	{
		_gameStateData.Schmeckels -= currencyToWithdraw;
		CurrencyChanged?.Invoke(_gameStateData.Schmeckels);
	}

}
